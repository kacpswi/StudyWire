using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Extensions;
using StudyWire.Application.Services.Interfaces;

namespace StudyWire.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ReturnLoginUserDto>> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _userService.LoginUserAsync(loginUserDto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ReturnLoginUserDto>> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("news")]
        public async Task<ActionResult<IEnumerable<ReturnNewsDto>>> GetUserNews()
        {
            var userId = User.GetUserId();
            var result = await _userService.GetAllUserNewsAsync(userId);
            return Ok(result);
        }
    }
}
