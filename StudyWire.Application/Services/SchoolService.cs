using AutoMapper;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.Exceptions;
using StudyWire.Application.Services.Interfaces;
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
        public async Task<ReturnSchoolDto> GetSchoolByIdAsync(int id)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(id);
            if (school == null) throw new NotFoundException("School not found");

            var dto = _mapper.Map<ReturnSchoolDto>(school);
            return dto;
        }
    }
}
