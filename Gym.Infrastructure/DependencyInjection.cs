using Gym.Application.Common.Interfaces;
using Gym.Infrastructure.Admins.Persistance;
using Gym.Infrastructure.Common.Persistence;
using Gym.Infrastructure.Gyms.Persistence;
using Gym.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<GymDbContext>(options =>
            options.UseSqlite("Data Source = GymManagment.db"));

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IGymRepository, GymRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymDbContext>());
        
        return services;
    }
}

