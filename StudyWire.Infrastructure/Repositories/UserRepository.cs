using Microsoft.AspNetCore.Identity;
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
    public class UserRepository : IUserRepository
    {
        private readonly StudyWireDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(StudyWireDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<(IEnumerable<AppUser>, int)> GetAllUsersWithRoleAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var query = GetAllSearchedAndSortedQuery(searchPhrase, sortBy, sortDirection);

            var users = await query
                .Include(u => u.School)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Include(n => n.School)
                .ToListAsync();

            var count = await query.CountAsync();

            return (users, count);
        }

        public async Task<AppUser?> GetUserWithSchoolAsync(int userId)
        {
            return await _context.Users.Include(u => u.School).FirstOrDefaultAsync(u => u.Id == userId);
        }

        private IQueryable<AppUser> GetAllSearchedAndSortedQuery(string? searchPhrase, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Users
                .Where(n => searchPhrase == null || (n.Name.ToUpper().Contains(searchPhrase.ToUpper())
                                            || n.Surename.ToUpper().Contains(searchPhrase.ToUpper())
                                            || n.Email.ToUpper().Contains(searchPhrase.ToUpper())));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<AppUser, object>>>
                {
                    { nameof(AppUser.Name), r => r.Name },
                    { nameof(AppUser.Surename), r => r.Surename },
                    { nameof(AppUser.SchoolId), r => r.SchoolId },
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
