using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.User
{
    public class ReturnUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surename { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
