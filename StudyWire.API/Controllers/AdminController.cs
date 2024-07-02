using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;

namespace StudyWire.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var result = await _adminService.GetUsersAsync();
            return Ok(result);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute]int id)
        {
            await _adminService.DeleteUserByIdAsync(id);
            return NoContent();
        }
    }
}
