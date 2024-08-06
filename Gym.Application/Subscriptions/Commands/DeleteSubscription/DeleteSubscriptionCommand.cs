using ErrorOr;
using MediatR;

namespace Gym.Application.Subscriptions.Commands.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid SubscriptionId)
     : IRequest<ErrorOr<Deleted>>;
