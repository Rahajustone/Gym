using ErrorOr;
using MediatR;

namespace Gym.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym.Domain.Gyms.Gym>>;
