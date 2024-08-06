using ErrorOr;
using Gym.Application.Common.Interfaces;
using MediatR;


namespace Gym.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionRepository _subscriptionsRepository;
    private readonly IGymRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteGymCommandHandler(
        ISubscriptionRepository subscriptionsRepository,
        IGymRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetGymByIdAsync(command.GymId);

        if (gym is null)
            return Error.NotFound(description: "Gym not found");

        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
            return Error.NotFound(description: "Subscription not found");

        if (!subscription.HasGym(command.GymId))
            return Error.Unexpected(description: "Gym not found");

        subscription.RemoveGym(command.GymId);

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymsRepository.RemoveGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}