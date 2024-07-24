using StudyWire.Domain.Entities;
using StudyWire.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<(IEnumerable<object?>, int)> GetAllUsersWithRoleAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    }
}
