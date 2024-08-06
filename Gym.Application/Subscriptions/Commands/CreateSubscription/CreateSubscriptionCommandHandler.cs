using MediatR;
using ErrorOr;

using Gym.Application.Subscription.Commands.CreateSubsciption;
using DomainSubscription = Gym.Domain.Subscriptions;
using Gym.Application.Common.Interfaces;

namespace Gym.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<DomainSubscription.Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IAdminRepository _adminsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IAdminRepository adminsRepository,
        IUnitOfWork unitOfWork
        )
    {
        _subscriptionRepository = subscriptionRepository;
        _adminsRepository = adminsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<DomainSubscription.Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

        if (admin == null)
            return Error.NotFound(description: "Admin not found");

        // Create A subscription
        var subscription = new DomainSubscription.Subscription
        (
            subscriptionType: request.SubscriptionType,
            adminId: request.AdminId
        );

        if (admin.SubscriptionId is not null)
            return Error.Conflict(description: "Admin already has an active subscription");

        admin.SetSubscription(subscription);

        // Add it to database
        await _subscriptionRepository.AddSubscriptionAsync(subscription);
        await _adminsRepository.UpdateAsync(admin);
        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }
}
