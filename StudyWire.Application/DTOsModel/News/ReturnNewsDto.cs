using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.News
{
    public class ReturnNewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SchoolId { get; set; }
    }
}
