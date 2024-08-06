
using Gym.Domain.Subscriptions;
using Throw;

namespace Gym.Domain.Admins;

public class Admin
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; set; }
    public Guid Id { get; set; }
    
    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null)
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
        Id = id ?? Guid.NewGuid();
    }

    private Admin() { }

    public void SetSubscription(Subscription subscriptions)
    {
        SubscriptionId.HasValue.Throw().IfTrue();
        SubscriptionId = subscriptions.Id;
    }

    public void DeleteSubscription(Guid subscriptionId)
    {
        SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);
        SubscriptionId = null;
    }
}
