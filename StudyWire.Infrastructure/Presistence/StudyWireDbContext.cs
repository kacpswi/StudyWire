using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities.User;
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

        public StudyWireDbContext(DbContextOptions<StudyWireDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .OwnsOne(a => a.Address);

        }
    }
}
