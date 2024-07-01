using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.User
{
    public class ReturnLoginUserDto
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Token { get; set; }
    }
}
