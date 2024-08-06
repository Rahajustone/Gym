using Gym.Application.Common.Interfaces;
using Gym.Domain.Admins;
using Gym.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Admins.Persistance
{
    public class AdminRepository : IAdminRepository
    {
        private readonly GymDbContext _gymDbContext;

        public AdminRepository(GymDbContext gymDbContext)
        {
            _gymDbContext = gymDbContext;
        }

        public async Task<Admin?> GetByIdAsync(Guid adminId)
        {
            return await _gymDbContext.Admins.FindAsync(adminId);
        }

        public Task UpdateAsync(Admin admin)
        {
            _gymDbContext.Admins.Update(admin);

            return Task.CompletedTask;
        }
    }
}
