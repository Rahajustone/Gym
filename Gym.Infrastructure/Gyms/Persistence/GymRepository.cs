using Gym.Application.Common.Interfaces;
using Gym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Gyms.Persistence;

public class GymRepository : IGymRepository
{
    private readonly GymDbContext _gymDbContext;

    public GymRepository(GymDbContext gymDbContext)
    {
        _gymDbContext = gymDbContext;
    }

    public async Task AddGymAsync(Domain.Gyms.Gym gym)
    {
        await _gymDbContext.Gyms.AddAsync(gym);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _gymDbContext.Gyms
            .AsNoTracking()
            .AnyAsync(g => g.Id == id);
    }

    public async Task<Domain.Gyms.Gym?> GetGymByIdAsync(Guid id)
    {
        return await _gymDbContext.Gyms.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Domain.Gyms.Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _gymDbContext.Gyms
            .Where( g => g.SubscriptionId == subscriptionId)
            .ToListAsync();
    }

    public Task RemoveGymAsync(Domain.Gyms.Gym gym)
    {
        _gymDbContext.Remove(gym);

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<Domain.Gyms.Gym> gyms)
    {
        _gymDbContext.RemoveRange(gyms);

        return Task.CompletedTask;
    }

    public Task UpdateGymAsync(Domain.Gyms.Gym gym)
    {
        _gymDbContext.Update(gym);

        return Task.CompletedTask;
    }
}
