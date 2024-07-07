using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Interfaces
{
    public interface INewsRepository
    {
        public Task<IEnumerable<News?>> GetAllNewsAsync();
        public Task<IEnumerable<News?>> GetAllNewsBySchoolIdAsync(int schoolId);
        public Task<News?> GetNewsByIdAsync(int newsId);
        public Task AddNewsAsync(News news);
        public void DeleteNews(News news);
        public Task<bool> SaveAsync();
    }
}
