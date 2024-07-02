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
        public async Task AddNews(News news)
        {
            await _context.AddAsync(news);
        }

        public async Task DeleteNewsAsync(News news)
        {
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<News?>> GetAllNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<IEnumerable<News?>> GetAllNewsBySchoolAsync(int schoolId)
        {
            return await _context.News.Where(n => n.Id == schoolId).ToListAsync();
        }

        public async Task<News?> GetNewsByIdAsync(int newsId)
        {
            return await _context.News.Where(n => n.Id == newsId).FirstOrDefaultAsync();
        }

        public async Task UpdateNews(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}
