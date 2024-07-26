using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Helpers.Pagination;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Exceptions;
using StudyWire.Domain.Interfaces;
using StudyWire.Domain.Models;
using System.Linq.Expressions;

namespace StudyWire.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminService(UserManager<AppUser> userManager, IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<PagedResult<ReturnUserWithRoles>> GetUsersWithRolesAsync(PagedQuery query)
        {
            var paginationResult = await _userRepository.GetAllUsersWithRoleAsync(query.SearchPhrase, query.PageSize,
                                                        query.PageNumber, query.SortBy, query.SortDirection);

            var usersWithRoles = new List<ReturnUserWithRoles>();
            foreach (var user in paginationResult.Item1)
            {
                string schoolName = "";

                if (user.School != null)
                {
                    schoolName = user.School.Name;
                }

                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(
                    new ReturnUserWithRoles
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surename = user.Surename,
                        Email = user.Email,
                        UserRoles = roles,
                        SchoolName = schoolName,
                        SchoolId = user.SchoolId
                    });
            }

            var result = new PagedResult<ReturnUserWithRoles>(usersWithRoles, paginationResult.Item2, query.PageSize, query.PageNumber);


            return result;
        }

        public async Task<ReturnUserWithRoles> EditUserRolesAsync(int userId, string roles)
        {
            if (string.IsNullOrEmpty(roles))
            {
                throw new BadRequestException("You must select at least one role");
            }

            var selectedRoles = roles.Split(",").ToArray();

            //var user = await _userManager.FindByIdAsync(userId.ToString());
            var user = await _userRepository.GetUserWithSchoolAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                throw new BadRequestException("Failed to add to roles");
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            var newRoles = await _userManager.GetRolesAsync(user);
            //var removedRoles = userRoles.Except(selectedRoles);

            if (!newRoles.Contains("Teacher") 
                && !newRoles.Contains("Student") 
                && !newRoles.Contains("School-Admin"))
            {
                user.SchoolId = null;
                await _userManager.UpdateAsync(user);
            }

            if (!result.Succeeded)
            {
                throw new BadRequestException("Failed to remove from roles");
            }

            string schoolName = "";

            if (user.SchoolId != null)
            {
                schoolName = user.School.Name;
            }

            var dto = new ReturnUserWithRoles()
            {
                Id = user.Id,
                Name = user.Name,
                Surename = user.Surename,
                Email = user.Email,
                UserRoles = await _userManager.GetRolesAsync(user),
                SchoolName = schoolName,
                SchoolId = user.SchoolId
            };

            return dto;
        }
    }
}

