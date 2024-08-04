using StudyWire.Application.DTOsModel.Group;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.DTOsModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<int> CreateGroupAsync(PostGroupDto groupDto, int userId, int schoolId);
        public Task<ReturnGroupDto> UpdateGroupAsync(PostGroupDto groupDto, int schoolId, int groupId, int userId);
        public Task DeleteGroupAsync(int schoolId, int groupId, int userId);
        public Task<ReturnGroupDto> GetGroupByIdAsync(int schoolId, int groupId, int userId);
        public Task<ReturnGroupDto> GetSchoolGroupByIdAsync(int schoolId, int groupId, int userId);
        public Task<IEnumerable<ReturnGroupDto>> GetAllGroupsAsync();
        public Task<IEnumerable<ReturnGroupDto>> GetAllSchoolGroupsAsync(int schoolId, int userId);
        public Task<ReturnGroupDto> GetGroupByNameAsync(int schoolId, string groupName, int userId);
    }
}
