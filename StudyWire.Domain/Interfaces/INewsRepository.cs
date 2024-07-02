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
        public Task<IEnumerable<News?>> GetAllNewsBySchoolAsync(int schoolId);
        public Task<News?> GetNewsByIdAsync(int newsId);
        public Task AddNews(News news);
        public Task UpdateNews(News news);
        public Task DeleteNewsAsync(News news);
    }
}
