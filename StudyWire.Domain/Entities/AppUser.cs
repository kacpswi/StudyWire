using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyWire.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public Address Address { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public int? SchoolId { get; set; }
        [JsonIgnore]
        public School? School { get; set;}
    }
}
