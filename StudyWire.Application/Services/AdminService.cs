using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Application.DTOsModel.News;
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

        public AdminService(UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
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

        public async Task<PagedResult<object>> GetUsersWithRolesAsync(PagedQuery query)
        {
            var paginationResult = await _userRepository.GetAllUsersWithRoleAsync(query.SearchPhrase, query.PageSize,
                                                        query.PageNumber, query.SortBy, query.SortDirection);

            var result = new PagedResult<object>(paginationResult.Item1, paginationResult.Item2, query.PageSize, query.PageNumber);
            return result;
        }

        public async Task EditUserRolesAsync(int userId, string roles)
        {
            if (string.IsNullOrEmpty(roles))
            {
                throw new BadRequestException("You must select at least one role");
            }

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByIdAsync(userId.ToString());

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

            if (!result.Succeeded)
            {
                throw new BadRequestException("Failed to remove from roles");
            }

        }
    }
}

