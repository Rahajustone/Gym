using Gym.Application.Common.Interfaces;
using Gym.Domain.Subscriptions;
using Gym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GymDbContext _gymDbContext;

        public SubscriptionRepository(GymDbContext gymDbContext)
        {
            _gymDbContext = gymDbContext;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _gymDbContext.Subscriptions.AddAsync(subscription);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _gymDbContext.Subscriptions
                .AsNoTracking()
                .AnyAsync(subscription => subscription.Id == id);
        }

        public async Task<Subscription?> GetByAdminIdAsync(Guid adminId)
        {
            return await _gymDbContext.Subscriptions
                .AsNoTracking()
                .FirstOrDefaultAsync( s => s.AdminId == adminId);
        }

        public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            return await _gymDbContext.Subscriptions.FindAsync(subscriptionId);
        }

        public async Task<List<Subscription>> ListAsync()
        {
            return await _gymDbContext.Subscriptions
                .AsNoTracking()
                .ToListAsync();
        }

        public Task RemoveSubscriptionAsync(Subscription subscription)
        {
            _gymDbContext.Remove(subscription);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Subscription subscription)
        {
            _gymDbContext.Update(subscription);

            return Task.CompletedTask;
        }
    }
}
