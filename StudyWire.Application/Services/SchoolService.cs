using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Exceptions;
using StudyWire.Domain.Interfaces;

namespace StudyWire.Application.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _schoolRepository = schoolRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<int> CreateSchoolAsync(PostSchoolDto schoolDto, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null) throw new NotFoundException("User not found");

            if (user.SchoolId != null) throw new BadRequestException("You cannot be admin of multiple schools");


            var school = _mapper.Map<School>(schoolDto);
            await _schoolRepository.AddSchoolAsync(school);
            school.CreatedById = userId;

            if (!await _schoolRepository.SaveAsync()) throw new BadRequestException("Failed to post school");

            var result = await _userManager.AddToRoleAsync(user, "Teacher");

            if (!result.Succeeded) throw new BadRequestException("Failed to add role Teacher");

            user.SchoolId = school.Id;
            result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new BadRequestException("Failed to update user");
            
            return school.Id;
        }

        public async Task DeleteSchoolAsync(int userId, int schoolId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User not found");

            var school = await _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null) throw new NotFoundException("School not found");

            if (school.CreatedById != userId) throw new BadRequestException("Cannot delete others' school");

            _schoolRepository.DeleteSchool(school);
            if (!await _schoolRepository.SaveAsync()) throw new BadRequestException("Unable to delete school");

            var result = await _userManager.RemoveFromRoleAsync(user, "Teacher");

            if (!result.Succeeded) throw new BadRequestException("Failed to add role Teacher");
            
            user.SchoolId = null;
            result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new BadRequestException("Failed to update user");
            
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

        public async Task<IEnumerable<ReturnUserDto>> GetSchoolMembersAsync(int schoolId)
        {
            var members = await _schoolRepository.GetSchoolMembersAsync(schoolId);

            if (members == null) throw new NotFoundException("Members not found!");

            var membersDto = _mapper.Map<IEnumerable<ReturnUserDto>>(members);

            return membersDto;
        }
    }
}
