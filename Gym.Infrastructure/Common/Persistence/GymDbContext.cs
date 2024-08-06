using Gym.Application.Common.Interfaces;
using Gym.Domain.Admins;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Common.Persistence
{
    public class GymDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Domain.Subscriptions.Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Domain.Gyms.Gym> Gyms { get; set; } = null!;


        public GymDbContext(DbContextOptions options) : base(options)
        {
        }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
