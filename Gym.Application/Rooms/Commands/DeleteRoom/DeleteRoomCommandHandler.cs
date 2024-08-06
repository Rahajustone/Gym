using ErrorOr;
using Gym.Application.Common.Interfaces;
using MediatR;

namespace Gym.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
    private readonly IGymRepository _gymRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(
        IGymRepository gymRepository,
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork
        )
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetGymByIdAsync(command.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }


        if (!gym.HasRoom(command.RoomId))
        {
            return Error.NotFound(description: "Room not found");
        }

        gym.RemoveRoom(command.RoomId);

        await _gymRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}

