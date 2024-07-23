using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities;
using StudyWire.Infrastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyWire.Infrastructure.Seeders
{
    public class NewsSeeder
    {
        public static async Task SeedNews(StudyWireDbContext context)
        {
            if (await context.News.AnyAsync()) return;


            var path = Environment.CurrentDirectory;
            var newsData = await File.ReadAllTextAsync(path + "/SeedData/NewsSeedData.json");

            var news = JsonSerializer.Deserialize<List<News>>(newsData);

            foreach (var item in news)
            {
                context.News.Add(item);
            }
            context.SaveChanges();
        }
    }
}
