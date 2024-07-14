using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Interfaces;
using StudyWire.Domain.Models;
using StudyWire.Infrastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<(IEnumerable<News?>,int)> GetAllNewsAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var query = GetAllSearchedAndSortedQuery(searchPhrase, sortBy, sortDirection);
            
            var news = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Include(n => n.School)
                .ToListAsync();

            var count = await query.CountAsync();

            return (news, count);

        }

        public async Task<(IEnumerable<News?>, int)> GetAllNewsBySchoolIdAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, int schoolId)
        {
            var query = GetAllSearchedAndSortedQuery(searchPhrase, sortBy, sortDirection)
                .Where(n => n.SchoolId == schoolId);

            var news = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Include(n => n.School)
                .ToListAsync();

            var count = await query.CountAsync();

            return (news, count);
        }

        public async Task<IEnumerable<News?>> GetAllUserNewsAsync(int userId)
        {
            return await _context.News
                .Where(n => n.CreatedById == userId)
                .OrderByDescending(n=> n.CreatedAt)
                .ToListAsync();
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

        private IQueryable<News> GetAllSearchedAndSortedQuery(string? searchPhrase, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.News
                .Where(n => searchPhrase == null || (n.Title.ToUpper().Contains(searchPhrase.ToUpper())
                                            || n.Description.ToUpper().Contains(searchPhrase.ToUpper())));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<News, object>>>
                {
                    { nameof(News.CreatedAt), r => r.CreatedAt },
                    { nameof(News.SchoolId), r => r.SchoolId },
                    { nameof(News.Author), r => r.Author }
                };

                var selectedColumn = columnsSelectors[sortBy];

                baseQuery = sortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            return baseQuery;
        }
    }
}
