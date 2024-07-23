using Microsoft.AspNetCore.Identity;
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
    public class UsersSeeder
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
                                            RoleManager<IdentityRole<int>> roleManager,
                                            StudyWireDbContext context)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                var roles = new List<IdentityRole<int>>
                {
                new IdentityRole<int>{Name = "Guest"},
                new IdentityRole<int>{Name = "Teacher"},
                new IdentityRole<int>{Name = "School-Admin"},
                new IdentityRole<int>{Name = "Admin"},
                new IdentityRole<int>{Name = "Parent"},
                new IdentityRole<int>{Name = "Student"},
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }


            if (!await userManager.Users.AnyAsync())
            {
                var path = Environment.CurrentDirectory;
                var usersData = await File.ReadAllTextAsync(path + "/SeedData/UsersSeedData.json");

                var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");

                    if(user.Name.Contains("guset")) await userManager.AddToRolesAsync(user, new[] { "Guest" });
                    else if(user.Name.Contains("teacher")) await userManager.AddToRolesAsync(user, new[] { "Teacher" });
                    else if (user.Name.Contains("student")) await userManager.AddToRolesAsync(user, new[] { "Student" });
                    else if (user.Name.Contains("school")) await userManager.AddToRolesAsync(user, new[] { "School-Admin" });
                    else await userManager.AddToRolesAsync(user, new[] { "Admin" });
                }
            }
            context.SaveChanges();
        }
    }
}
