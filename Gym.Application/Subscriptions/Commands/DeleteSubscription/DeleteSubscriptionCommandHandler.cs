using ErrorOr;
using Gym.Application.Common.Interfaces;
using Gym.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler :
    IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly IAdminRepository _adminsRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(
        IAdminRepository adminsRepository,
        ISubscriptionRepository subscriptionRepository,
        IGymRepository gymRepository,
        IUnitOfWork unitOfWork)
    {
        _adminsRepository = adminsRepository;
        _subscriptionRepository = subscriptionRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(
        DeleteSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        // Check subscription if exist or not
        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);
        if (subscription is null)
            return Error.NotFound(description: $"Subscription not found with {command.SubscriptionId} id.");

        var admin = await _adminsRepository.GetByIdAsync(subscription.AdminId);

        if (admin is null)
            return Error.Unexpected(description: "Admin not found.");

        admin.DeleteSubscription(command.SubscriptionId);

        var gymsToDelete = await _gymRepository.ListBySubscriptionIdAsync(command.SubscriptionId);

        await _adminsRepository.UpdateAsync(admin);
        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _gymRepository.RemoveRangeAsync(gymsToDelete);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }

}
