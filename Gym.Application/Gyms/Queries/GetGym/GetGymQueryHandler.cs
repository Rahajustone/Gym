using ErrorOr;
using Gym.Application.Common.Interfaces;
using MediatR;

namespace Gym.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym.Domain.Gyms.Gym>>
{
    private readonly IGymRepository _gymsRepository;
    private readonly ISubscriptionRepository _subscriptionsRepository;

    public GetGymQueryHandler(IGymRepository gymsRepository, ISubscriptionRepository subscriptionsRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Gym.Domain.Gyms.Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
        {
            return Error.NotFound("Subscription not found");
        }

        if (await _gymsRepository.GetGymByIdAsync(request.GymId) is not Gym.Domain.Gyms.Gym gym)
        {
            return Error.NotFound(description: "Gym not found");
        }

        return gym;
    }
}