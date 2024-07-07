using StudyWire.Application.DTOsModel.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services.Interfaces
{
    public interface ISchoolService
    {
        public Task<ReturnSchoolDto> GetSchoolByIdAsync(int id);
        public Task<IEnumerable<ReturnSchoolDto>> GetSchoolsAsync();
        public Task<int> CreateSchoolAsync(PostSchoolDto schoolDto, int userId);
        public Task<ReturnSchoolDto> UpdateSchoolAsync(PostSchoolDto schoolDto, int userId,  int schoolId);
        public Task DeleteSchoolAsync(int userId, int schoolId);
    }
}
