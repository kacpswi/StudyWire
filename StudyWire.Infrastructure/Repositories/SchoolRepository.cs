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
        public async Task<School?> GetSchoolByIdAsync(int schoolId)
        {
            return await _context.Schools.Include(s => s.News).FirstOrDefaultAsync(s => s.Id == schoolId);
        }
    }
}
