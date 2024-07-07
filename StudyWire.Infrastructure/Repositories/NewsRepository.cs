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
    public class NewsRepository : INewsRepository
    {
        private readonly StudyWireDbContext _context;

        public NewsRepository(StudyWireDbContext context)
        {
            _context = context;
        }
        public async Task AddNewsAsync(News news)
        {
            await _context.AddAsync(news);
        }

        public void DeleteNews(News news)
        {
            _context.News.Remove(news);
        }

        public async Task<IEnumerable<News?>> GetAllNewsAsync()
        {
            return await _context.News
                .Include(n => n.School)
                .ToListAsync();
        }

        public async Task<IEnumerable<News?>> GetAllNewsBySchoolIdAsync(int schoolId)
        {
            var news = await _context.News
                .Include(n => n.School)
                .Where(n => n.SchoolId == schoolId)
                .ToListAsync();
            return news;
        }

        public async Task<News?> GetNewsByIdAsync(int newsId)
        {
            return await _context.News
                .Include(n => n.School)
                .FirstOrDefaultAsync(n => n.Id == newsId);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
