using ErrorOr;
using Gym.Application.Common.Interfaces;
using MediatR;

namespace Gym.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym.Domain.Gyms.Gym>>>
{
    private readonly IGymRepository _gymRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public ListGymsQueryHandler(IGymRepository gymRepository,
        ISubscriptionRepository subscriptionRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
    }
    public async Task<ErrorOr<List<Domain.Gyms.Gym>>> Handle(ListGymsQuery query, 
        CancellationToken cancellationToken)
    {
        if (!await _subscriptionRepository.ExistsAsync(query.SubscriptionId))
            return Error.NotFound(description: "Subscription not found");

        return await _gymRepository.ListBySubscriptionIdAsync(query.SubscriptionId);
    }
}
