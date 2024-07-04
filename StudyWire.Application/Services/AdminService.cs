﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<AppUser> _userManager;

        public AdminService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                throw new NotFoundException("User not found.");
            }
            await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
