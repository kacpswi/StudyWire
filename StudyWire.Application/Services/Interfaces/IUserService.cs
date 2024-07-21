using StudyWire.Application.DTOsModel.News;
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
        public Task<ReturnLoginUserDto> LoginUserAsync(LoginUserDto loginUserDto);
        public Task<ReturnLoginUserDto> RegisterUserAsync(RegisterUserDto registerUserDto);
        public Task<IEnumerable<ReturnNewsDto>> GetAllUserNewsAsync(int userId);
    }
}
