using DomainSubscription = Gym.Domain.Subscriptions;

namespace Gym.Application.Common.Interfaces;

public interface ISubscriptionRepository
{
    Task AddSubscriptionAsync(DomainSubscription.Subscription subscription);
    Task<DomainSubscription.Subscription?> GetByIdAsync(Guid subscriptionId);
    Task RemoveSubscriptionAsync(DomainSubscription.Subscription subscription);
    Task<bool> ExistsAsync(Guid id);
    Task<DomainSubscription.Subscription?> GetByAdminIdAsync(Guid adminId);
    Task<List<DomainSubscription.Subscription>> ListAsync();
    Task UpdateAsync(DomainSubscription.Subscription subscription);
}
