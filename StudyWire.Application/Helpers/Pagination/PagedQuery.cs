using StudyWire.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Helpers.Pagination
{
    public class PagedQuery
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; } 
    }
}
