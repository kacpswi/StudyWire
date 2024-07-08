using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.Helpers.Pagination;
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
        public Task<ReturnNewsDto> GetNewsByIdAsync(int schoolId, int newsId);
        public Task<PagedResult<ReturnNewsDto>> GetNewsBySchoolIdAsync(PagedQuery query, int userId);
        public Task<PagedResult<ReturnNewsDto>> GetAllNewsAsync(PagedQuery query);
        public Task<int> CreateNewsAsync(PostNewsDto newsDto, int userId, int schoolId);
        public Task<ReturnNewsDto> UpdateNewsAsync(PostNewsDto newsDto, int newsId, int userId, int schoolId);
        public Task DeleteNewsAsync(int newsId, int schoolId, int userId);
    }
}
