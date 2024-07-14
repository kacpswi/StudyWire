using Microsoft.AspNetCore.Http;
using StudyWire.Application.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyWire.Application.Extensions
{
    public static class HttpExtensions
    {
        public record PaginationHeader(int CurrentPage, int ItemsPerPage, int TotalItems, int TotalPages);
        public static void AddPaginationHeader<T>(this HttpResponse response, PagedResult<T> data)
        {
            var paginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalItemCount, data.TotalPages);

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }

        public static void ExposeLocationHeader(this HttpResponse response)
        {
            response.Headers.Append("Access-Control-Expose-Headers", "Location");
        }
    }
}
