using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<AppUser>> GetUsersAsync();
        public Task<AppUser?> GetUserByIdAsync(int id);
        public Task DeleteUser(AppUser user);
    }
}
