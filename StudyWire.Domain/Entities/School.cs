using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Domain.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedById { get; set; }
        public Address Address { get; set; }
        public List<AppUser> Members { get; set; }
        public List<News> News { get; set; }
        public List<Group> Groups { get; set; }
    }
}
