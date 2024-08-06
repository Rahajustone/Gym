using ErrorOr;
using MediatR;
using System;

namespace Gym.Application.Subscriptions.Queries.GetSubscription;

public record GetSubscriptionQuery(Guid SubsciptionId) 
    : IRequest<ErrorOr<Gym.Domain.Subscriptions.Subscription>>;

