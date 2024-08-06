using Gym.Domain.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Common.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByIdAsync(Guid adminId);
        Task UpdateAsync(Admin admin);
    }
}
