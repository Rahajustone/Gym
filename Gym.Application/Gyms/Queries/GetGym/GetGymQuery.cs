using ErrorOr;
using MediatR;

namespace Gym.Application.Gyms.Queries.GetGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId)
    : IRequest<ErrorOr<Gym.Domain.Gyms.Gym>>;