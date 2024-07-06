using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Domain.Entities;
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

            if (!await context.Schools.AnyAsync())
            {
                var schools = new List<School>
                    {
                        new School()
                        {
                            Name = "School A",
                            Address = new Address
                            {
                                PhoneNumber = "Phone A",
                                City = "City A",
                                PostalCode = "PostalCode A",
                                Street = "Street A",
                            }
                        },
                        new School()
                        {
                            Name = "School B",
                            Address = new Address
                            {
                                PhoneNumber = "Phone B",
                                City = "City B",
                                PostalCode = "PostalCode B",
                                Street = "Street B",
                            }
                    }
                };
                context.Schools.AddRange(schools);
                context.SaveChanges();
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

                var teacher = new AppUser
                {
                    UserName = "teacher",
                    Name = "teacher",
                    Surename = "teacher",
                    Email = "teacher",
                    SchoolId = 1,
                };
                var teacherResult = await userManager.CreateAsync(teacher, "Pa$$w0rd");
                await userManager.AddToRolesAsync(teacher, new[] { "Teacher" });

                var studentA = new AppUser
                {
                    UserName = "studentA",
                    Name = "studentA",
                    Surename = "studentA",
                    Email = "studentA",
                    SchoolId = 1,
                };
                var studentAResult = await userManager.CreateAsync(studentA, "Pa$$w0rd");
                await userManager.AddToRolesAsync(studentA, new[] { "Student" });

                var studentB = new AppUser
                {
                    UserName = "studentB",
                    Name = "studentB",
                    Surename = "studentB",
                    Email = "studentB",
                    SchoolId = 2,
                };
                var studentBResult = await userManager.CreateAsync(studentB, "Pa$$w0rd");
                await userManager.AddToRolesAsync(studentB, new[] { "Student" });

                var admin = new AppUser
                {
                    UserName = "admin",
                    Name = "admin",
                    Surename = "admin",
                    Email = "admin"
                };
                var adminResult = await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Admin" });


            }

        }

    }
}
