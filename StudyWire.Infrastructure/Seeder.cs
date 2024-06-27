using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities.User;
using StudyWire.Infrastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyWire.Infrastructure
{
    public class Seeder
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
                var guest = new AppUser
                {
                    UserName = "guest",
                    Name = "guest",
                    Surename = "guest",
                    Email = "guest",
                };
                var guestResult = await userManager.CreateAsync(guest, "Pa$$w0rd");
                await userManager.AddToRolesAsync(guest, new[] { "Guest" });

                var admin = new AppUser
                {
                    UserName = "admin",
                    Name = "admin",
                    Surename = "admin",
                    Email = "admin",
                };
                var adminResult = await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Admin" });
            }

        }

    }
}
