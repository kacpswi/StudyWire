using StudyWire.Application.DTOsModel.User;
using StudyWire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.Group
{
    public class ReturnGroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int SchoolId { get; set; }
        public List<ReturnUserDto> Students { get; set; }
    }
}
