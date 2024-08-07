using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Extensions;
using StudyWire.Application.Helpers.Pagination;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;

namespace StudyWire.API.Controllers
{
    
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        //AllowAnonymous only for testing
        [AllowAnonymous]
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var result = await _adminService.GetUsersAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute]int id)
        {
            await _adminService.DeleteUserByIdAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles([FromQuery] PagedQuery query)
        {
            var result = await _adminService.GetUsersWithRolesAsync(query);
            Response.AddPaginationHeader(result);
            return Ok(result.Items);
        }

        [HttpPost]
        [Route("edit-roles/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReturnUserWithRoles>> EditRoles([FromRoute] int userId, [FromQuery] string roles)
        {
            var newRoles = await _adminService.EditUserRolesAsync(userId, roles);
            return Ok(newRoles);
        }

        [HttpPost]
        [Route("edit-school/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReturnUserDto>> EditSchool([FromRoute] int userId, [FromQuery] string schoolId)
        {
            var newSchool = await _adminService.EditUserSchoolAsync(userId, schoolId);
            return Ok(new { Name = newSchool });
        }

        [HttpPost]
        [Route("add-user")]
        [Authorize(Roles = "Admin,School-Admin")]
        public async Task<ActionResult<ReturnUserDto>> CreateUser([FromBody]RegisterUserDto dto, [FromQuery] int schoolId)
        {
            var userId = User.GetUserId();
            var result = await _adminService.CreateUserAsync(schoolId, userId, dto);
            return Created("", result);
        }
    }
}
