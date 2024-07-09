using AutoMapper;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Exceptions;
using StudyWire.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateSchoolAsync(PostSchoolDto schoolDto, int userId)
        {
            var school = _mapper.Map<School>(schoolDto);
            await _schoolRepository.AddSchoolAsync(school);
            school.CreatedById = userId;

            if (!await _schoolRepository.SaveAsync()) throw new BadRequestException("Failed to post school");

            return school.Id;
        }

        public async Task DeleteSchoolAsync(int userId, int schoolId)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null) throw new NotFoundException("School not found");

            if (school.CreatedById != userId) throw new BadRequestException("Cannot delete others' school");

            _schoolRepository.DeleteSchool(school);
            if (!await _schoolRepository.SaveAsync()) throw new BadRequestException("Unable to delete school");
        }

        public async Task<ReturnSchoolDto> GetSchoolByIdAsync(int id)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(id);
            if (school == null) throw new NotFoundException("School not found");

            var dto = _mapper.Map<ReturnSchoolDto>(school);
            return dto;
        }

        public async Task<IEnumerable<ReturnSchoolDto>> GetSchoolsAsync()
        {
            var schools = await _schoolRepository.GetAllSchoolsAsync();
            var dtos = _mapper.Map<IEnumerable<ReturnSchoolDto>>(schools);
            return dtos;
        }

        public async Task<ReturnSchoolDto> UpdateSchoolAsync(PostSchoolDto schoolDto, int userId, int schoolId)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school != null)
            {
                if (userId != school.CreatedById) throw new BadRequestException("Cannot edit others' schools");

                var newSchool = _mapper.Map(schoolDto, school);

                if (!await _schoolRepository.SaveAsync())
                    throw new BadRequestException("Unable to update news");

                var newSchoolDto = _mapper.Map<ReturnSchoolDto>(newSchool);
                return newSchoolDto;

            }
            else
            {
                throw new NotFoundException("School not found");
            }
            
        }
    }
}
