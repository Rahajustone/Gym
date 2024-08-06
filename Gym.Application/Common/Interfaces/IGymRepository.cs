using GymEntity = Gym.Domain.Gyms;

namespace Gym.Application.Common.Interfaces;

public interface IGymRepository
{
    Task AddGymAsync(GymEntity.Gym gym);
    Task<GymEntity.Gym> GetGymByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<List<GymEntity.Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
    Task UpdateGymAsync(GymEntity.Gym gym);
    Task RemoveGymAsync(GymEntity.Gym gym);
    Task RemoveRangeAsync(List<GymEntity.Gym> gyms);
}
