using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Domain.Entities;

namespace StudyWire.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PostNewsDto, News>();
            CreateMap<News, ReturnNewsDto>();

            CreateMap<RegisterUserDto, AppUser>()
                .ForMember(a => a.Address, c => c.MapFrom(dto => new Address()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street, PhoneNumber = dto.PhoneNumber }));

            CreateMap<School, ReturnSchoolDto>();
            CreateMap<PostSchoolDto, School>()
                .ForMember(a => a.Address, s => s.MapFrom(dto => new Address()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street, PhoneNumber = dto.PhoneNumber }));

            CreateMap<AppUser, ReturnUserDto>();
        }
    }
}
