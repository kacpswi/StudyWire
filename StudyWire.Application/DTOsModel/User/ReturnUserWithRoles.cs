using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.User
{
    public class ReturnUserWithRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public IList<string> UserRoles { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
