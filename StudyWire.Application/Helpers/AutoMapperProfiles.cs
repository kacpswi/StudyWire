using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Domain.Entities;

namespace StudyWire.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PostNewsDto, News>();
            CreateMap<News, ReturnNewsDto>();
        }
    }
}
