using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Helpers.Pagination;
using StudyWire.Domain.Entities;
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
        public Task<PagedResult<ReturnUserWithRoles>> GetUsersWithRolesAsync(PagedQuery query);
        public Task<ReturnUserWithRoles> EditUserRolesAsync(int userId, string roles);
        public Task<string> EditUserSchoolAsync(int userId, string schoolId);
    }
}
