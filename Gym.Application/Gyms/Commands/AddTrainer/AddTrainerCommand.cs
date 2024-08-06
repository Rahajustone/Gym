using ErrorOr;
using MediatR;

namespace Gym.Application.Gyms.Commands.AddTrainer;

public record AddTrainerCommand(Guid SubscriptionId, Guid GymId, Guid TrainerId)
     : IRequest<ErrorOr<Success>>;
