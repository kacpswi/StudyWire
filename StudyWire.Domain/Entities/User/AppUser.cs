using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyWire.Domain.Entities.User
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public Address Address { get; set; }
    }
}
