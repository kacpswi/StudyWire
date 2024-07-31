using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Interfaces
{
    public interface ISchoolRepository
    {
        public Task<School?> GetSchoolByIdAsync(int schoolId);
        public Task<IEnumerable<School?>> GetAllSchoolsAsync();
        public Task AddSchoolAsync(School school);
        public void DeleteSchool(School school);
        public Task<School?> GetSchoolByNameAsync(string schoolName);
        public Task<IEnumerable<AppUser>?> GetSchoolMembersAsync(int schoolId);
        public Task<bool> SaveAsync();
    }
}
