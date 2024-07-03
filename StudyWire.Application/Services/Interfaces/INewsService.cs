using StudyWire.Application.DTOsModel.News;
using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services.Interfaces
{
    public interface INewsService
    {
        public Task<ReturnNewsDto> GetNewsByIdAsync(int id);
        public Task<IEnumerable<ReturnNewsDto>> GetNewsBySchoolIdAsync(int userId);
        public Task<IEnumerable<ReturnNewsDto>> GetAllNewsAsync();
        public Task<int> CreateNewsAsync(PostNewsDto newsDto, int userId, int schoolId);
        public Task<ReturnNewsDto> UpdateNewsAsync(PostNewsDto newsDto, int newsId, int userId, int schoolId);
        public Task DeleteNewsAsync(int newsId, int schoolId, int userId);
    }
}
