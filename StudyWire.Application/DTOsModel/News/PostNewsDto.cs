using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.News
{
    public class PostNewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
    }
}
