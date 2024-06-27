using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Exceptions;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities.User;
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

        public UserService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid Email or Password.");
            }

            var result = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            return await _tokenService.CreateToken(user);
        }

        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if (await UserExists(registerUserDto.Email))
            {
                throw new BadRequestException("Email is taken.");
            }
            else if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new BadRequestException("Password and Confirm Password must be the same.");
            }


            var user = new AppUser
            {
                Email = registerUserDto.Email,
                Name = registerUserDto.Name,
                Surename = registerUserDto.Surename,
                Address = new Address
                {
                    PhoneNumber = registerUserDto.PhoneNumber,
                    City = registerUserDto.City,
                    PostalCode = registerUserDto.PostalCode,
                    Street  = registerUserDto.Street,
                }
            };

            //var user = _mapper.Map<User>(registerUserDto);
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

        }

        private async Task<bool> UserExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.NormalizedEmail == email.ToUpper());
        }
    }
}
