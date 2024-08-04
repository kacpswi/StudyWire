using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Interfaces
{
    public interface IGroupRepository
    {
        public Task<Group?> GetGroupByIdAsync(int groupId);
        public Task<Group?> GetSchoolGroupByIdAsync(int schoolId, int groupId);
        public Task<IEnumerable<Group?>> GetAllGroupsAsync();
        public Task<IEnumerable<Group?>> GetAllSchoolGroupsAsync(int schoolId);
        public Task AddGroupAsync(Group group);
        public void DeleteGroup(Group group);
        public Task<Group?> GetGroupByNameAsync(string groupName);
        public Task<bool> SaveAsync();
    }
}
