using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services
{
    public class UserService : IUserService
    {
        private UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ReturnLoginUserDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid Email or Password.");
            }

            var result = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid Email or Password.");
            }

            string token = await _tokenService.CreateToken(user);

            var userDto = new ReturnLoginUserDto()
            {
                Name = user.Name,
                Surename = user.Surename,
                Token = token,
                SchoolId = user.SchoolId
            };

            return userDto;
        }

        public async Task<ReturnLoginUserDto> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new BadRequestException("Password and Confirm Password must be the same.");
            }
            else if (await UserExists(registerUserDto.Email))
            {
                throw new BadRequestException("Email is taken.");
            }

            var user = _mapper.Map<AppUser>(registerUserDto);
            user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                    stringBuilder.Append(" ");
                }
                var str = stringBuilder.ToString();
                throw new BadRequestException(str);
            }

            await _userManager.AddToRoleAsync(user, "Guest");
            var token = await _tokenService.CreateToken(user);
            return new ReturnLoginUserDto()
            {
                Name = user.UserName,
                Surename = user.Surename,
                Token = token,
                SchoolId = user.SchoolId
            };


        }

        private async Task<bool> UserExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.NormalizedEmail == email.ToUpper());
        }
    }
}
