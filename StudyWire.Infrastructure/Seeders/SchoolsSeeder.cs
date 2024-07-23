using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities;
using StudyWire.Infrastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyWire.Infrastructure.Seeders
{
    public class SchoolsSeeder
    {
        public static async Task SeedSchools(StudyWireDbContext context)
        {
            if (await context.Schools.AnyAsync()) return;


            var path = Environment.CurrentDirectory;
            var schoolData = await File.ReadAllTextAsync(path + "/SeedData/SchoolsSeedData.json");

            var schools = JsonSerializer.Deserialize<List<School>>(schoolData);

            foreach (var school in schools)
            {
                context.Schools.Add(school);
            }
            context.SaveChanges();
        }
    }
}
