using StudyWire.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services.Interfaces
{
    public interface IAdminService
    {
        public Task <IEnumerable<AppUser>> GetUsersAsync();
        public Task DeleteUserByIdAsync(int id);
    }
}
