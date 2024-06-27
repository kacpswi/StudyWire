using StudyWire.Application.DTOsModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> LoginUserAsync(LoginUserDto loginUserDto);
        public Task RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}
