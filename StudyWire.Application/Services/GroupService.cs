using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyWire.Application.DTOsModel.Group;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Exceptions;
using StudyWire.Domain.Interfaces;
using System.Text.RegularExpressions;


namespace StudyWire.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISchoolRepository _schoolRepository;

        public GroupService(IMapper mapper, IGroupRepository groupRepository,
                UserManager<AppUser> userManager, ISchoolRepository schoolRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _userManager = userManager;
            _schoolRepository = schoolRepository;
        }
        public async Task<int> CreateGroupAsync(PostGroupDto groupDto, int userId, int schoolId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User not found");

            if (user.SchoolId != schoolId) throw new BadRequestException("Cannot create group for others schools");

            var school = _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null) throw new NotFoundException("School not found");

            var group = _mapper.Map<Domain.Entities.Group>(groupDto);
            group.SchoolId = schoolId;

            await _groupRepository.AddGroupAsync(group);
            await _groupRepository.SaveAsync();
            return group.Id;

        }

        public async Task DeleteGroupAsync(int schoolId, int groupId, int userId)
        {
            var group = await GetGroupWithChceckAsync(schoolId, groupId, userId);

            _groupRepository.DeleteGroup(group);
            if (!await _groupRepository.SaveAsync()) throw new BadRequestException("Failed to delete group");

        }

        public async Task<IEnumerable<ReturnGroupDto>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAllGroupsAsync();

            var dtos = _mapper.Map<IEnumerable<ReturnGroupDto>>(groups);
            return dtos;
        }

        public async Task<IEnumerable<ReturnGroupDto>> GetAllSchoolGroupsAsync(int schoolId, int userId)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null) throw new NotFoundException("School not found");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User not found");

            if (user.SchoolId != schoolId) throw new BadRequestException("Cannot perform actions on groups of other schools");

            var groups = await _groupRepository.GetAllSchoolGroupsAsync(schoolId);
            return _mapper.Map<IEnumerable<ReturnGroupDto>>(groups);
        }

        public async Task<ReturnGroupDto> GetGroupByIdAsync(int schoolId, int groupId, int userId)
        {
            var group = await GetGroupWithChceckAsync(schoolId, groupId, userId);
            return _mapper.Map<ReturnGroupDto>(group);
        }

        public async Task<ReturnGroupDto> GetGroupByNameAsync(int schoolId, string groupName, int userId)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null) throw new NotFoundException("School not found");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User not found");
            if (user.SchoolId != schoolId) throw new BadRequestException("Cannot get groups of other schools");

            var group = await _groupRepository.GetGroupByNameAsync(groupName);
            if (group == null) throw new NotFoundException("Group not found");

            var groupDto = _mapper.Map<ReturnGroupDto>(group);
            return groupDto;
        }

        public async Task<ReturnGroupDto> GetSchoolGroupByIdAsync(int schoolId, int groupId, int userId)
        {
            var group = await GetGroupWithChceckAsync(schoolId, groupId, userId);

            var groupDto = _mapper.Map<ReturnGroupDto>(group);
            return groupDto;
        }

        public async Task<ReturnGroupDto> UpdateGroupAsync(PostGroupDto groupDto, int schoolId, int groupId, int userId)
        {
            var group = await GetGroupWithChceckAsync(schoolId, groupId, userId);

            var newGroup = _mapper.Map(groupDto, group);

            if (!await _groupRepository.SaveAsync()) throw new BadRequestException("Unable to update group");

            var newGroupDto = _mapper.Map<ReturnGroupDto>(newGroup);
            return newGroupDto;
        }

        private async Task<Domain.Entities.Group> GetGroupWithChceckAsync(int schoolId, int groupId, int userId)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null) throw new NotFoundException("School not found");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User not found");
            if (user.SchoolId != schoolId) throw new BadRequestException("Cannot perform actions on groups of other schools");

            var group = await _groupRepository.GetSchoolGroupByIdAsync(schoolId, groupId);
            if (group == null) throw new NotFoundException("Group not found");

            return group;
        }
    }
}
