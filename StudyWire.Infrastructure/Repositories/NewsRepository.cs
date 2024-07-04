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

        public void DeleteNews(News news)
        {
            _context.Newses.Remove(news);
        }

        public async Task<IEnumerable<News?>> GetAllNewsesAsync()
        {
            return await _context.Newses
                .Include(n => n.School)
                .ToListAsync();
        }

        public async Task<IEnumerable<News?>> GetAllNewsesBySchoolIdAsync(int schoolId)
        {
            var newses = await _context.Newses
                .Include(n => n.School)
                .Where(n => n.SchoolId == schoolId)
                .ToListAsync();
            return newses;
        }

        public async Task<News?> GetNewsByIdAsync(int newsId)
        {
            return await _context.Newses
                .Include(n => n.School)
                .FirstOrDefaultAsync(n => n.Id == newsId);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
