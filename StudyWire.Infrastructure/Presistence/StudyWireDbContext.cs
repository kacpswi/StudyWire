using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Infrastructure.Presistence
{
    public class StudyWireDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int,
            IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>,
            IdentityUserToken<int>>
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<News> Newses { get; set; }

        public StudyWireDbContext(DbContextOptions<StudyWireDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<School>()
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Members)
                .WithOne(u => u.School)
                .HasForeignKey(u => u.SchoolId);

            modelBuilder.Entity<School>()
                .HasMany(s => s.News)
                .WithOne(n => n.School)
                .HasForeignKey(n => n.SchoolId);
        }
    }
}
