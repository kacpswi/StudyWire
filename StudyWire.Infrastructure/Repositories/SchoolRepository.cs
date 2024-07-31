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
    public class SchoolRepository : ISchoolRepository
    {
        private readonly StudyWireDbContext _context;

        public SchoolRepository(StudyWireDbContext context)
        {
            _context = context;
        }

        public async Task AddSchoolAsync(School school)
        {
            await _context.AddAsync(school);
        }

        public void DeleteSchool(School school)
        {
            _context.Remove(school);
        }

        public async Task<IEnumerable<School?>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        public async Task<School?> GetSchoolByIdAsync(int schoolId)
        {
            return await _context.Schools.Include(s => s.News).FirstOrDefaultAsync(s => s.Id == schoolId);
        }

        public async Task<School?> GetSchoolByNameAsync(string schoolName)
        {
            return await _context.Schools.FirstOrDefaultAsync(s => s.Name == schoolName);
        }

        public async Task<IEnumerable<AppUser>?> GetSchoolMembersAsync(int schoolId)
        {
            return await _context.Schools.Include(s => s.Members).Where(s => s.Id == schoolId)
                .SelectMany(s => s.Members)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
