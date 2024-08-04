using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Interfaces;
using StudyWire.Infrastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly StudyWireDbContext _context;

        public GroupRepository(StudyWireDbContext context)
        {
            _context = context;
        }
        public async Task AddGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public void DeleteGroup(Group group)
        {
            _context.Groups.Remove(group);
        }

        public async Task<IEnumerable<Group?>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<IEnumerable<Group?>> GetAllSchoolGroupsAsync(int schoolId)
        {
            return await _context.Groups.Where(g => g.SchoolId == schoolId).ToListAsync();
        }

        public async Task<Group?> GetGroupByIdAsync(int groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<Group?> GetGroupByNameAsync(string groupName)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.GroupName == groupName);
        }

        public async Task<Group?> GetSchoolGroupByIdAsync(int schoolId, int groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.SchoolId == schoolId && g.Id == groupId);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
