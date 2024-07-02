using Microsoft.AspNetCore.Identity;
using StudyWire.Application.Exceptions;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if(user == null)
            {
                throw new NotFoundException("User not found.");
            }

            await _userRepository.DeleteUser(user);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
    }
}
