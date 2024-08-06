using ErrorOr;
using Gym.Domain.Subscriptions;
using MediatR;

namespace Gym.Application.Subscription.Commands.CreateSubsciption;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) 
    : IRequest<ErrorOr<Gym.Domain.Subscriptions.Subscription>>;
