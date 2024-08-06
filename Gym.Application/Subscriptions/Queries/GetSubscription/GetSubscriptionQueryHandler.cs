using ErrorOr;
using Gym.Application.Common.Interfaces;
using MediatR;
using DomainSubscription = Gym.Domain.Subscriptions;

namespace Gym.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<DomainSubscription.Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<ErrorOr<DomainSubscription.Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var subcscription = await _subscriptionRepository.GetByIdAsync(request.SubsciptionId);

            return subcscription is null ?
                Error.NotFound(description: "Subscription Not found")
                : subcscription;
        }
    }
}
