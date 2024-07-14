using StudyWire.Domain.Entities;
using StudyWire.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Interfaces
{
    public interface INewsRepository
    {
        public Task<(IEnumerable<News?>, int)> GetAllNewsAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        public Task<(IEnumerable<News?>, int)> GetAllNewsBySchoolIdAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, int schoolId);
        public Task<News?> GetNewsByIdAsync(int newsId);
        public Task<IEnumerable<News?>> GetAllUserNewsAsync(int userId);
        public Task AddNewsAsync(News news);
        public void DeleteNews(News news);
        public Task<bool> SaveAsync();
    }
}
