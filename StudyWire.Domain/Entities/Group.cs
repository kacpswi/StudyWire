using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public List<AppUser> Students { get; set; }
    }
}
