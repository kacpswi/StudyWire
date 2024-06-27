using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.DTOsModel.User;
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
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _userService.LoginUserAsync(loginUserDto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _userService.RegisterUserAsync(dto);
            return Ok();
        }
    }
}
