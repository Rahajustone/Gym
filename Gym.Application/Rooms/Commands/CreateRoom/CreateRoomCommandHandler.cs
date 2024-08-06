using ErrorOr;
using Gym.Application.Common.Interfaces;
using Gym.Domain.Rooms;
using MediatR;

namespace Gym.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IGymRepository gymRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetGymByIdAsync(command.GymId);
        if (gym == null)
            return Error.NotFound(description: "Gym not found");

        var subscription = await _subscriptionRepository.GetByIdAsync(gym.SubscriptionId);
        if (subscription == null)
            return Error.NotFound(description: "Subscription not found");

        var room = new Room(
            name: command.RoomName,
            gymId: gym.Id,
            maxDailySessions: subscription.GetMaxDailySessions());

        var addGymResult = gym.AddRoom(room);
        if (addGymResult.IsError)
            return addGymResult.Errors;

        await _gymRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return room;
    }
}