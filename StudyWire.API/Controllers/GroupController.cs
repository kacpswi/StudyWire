using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StudyWire.Application.DTOsModel.Group;
using StudyWire.Application.Extensions;
using StudyWire.Application.Services.Interfaces;

namespace StudyWire.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/schools/{schoolId}/groups")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [Authorize(Roles = "School-Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateGroup([FromBody] PostGroupDto dto, [FromRoute]int schoolId)
        {
            var userId = User.GetUserId();
            var result = await _groupService.CreateGroupAsync(dto, userId, schoolId);
            Response.ExposeLocationHeader();
            return Created($"/api/schools/{schoolId}/groups/{result}", null);
        }

        [HttpPut]
        [Route("{groupId}")]
        public async Task<ActionResult<ReturnGroupDto>> UpdateGroup([FromBody] PostGroupDto dto, [FromRoute] int schoolId, [FromRoute]int groupId)
        {
            var userId = User.GetUserId();
            var result = await _groupService.UpdateGroupAsync(dto, schoolId, groupId, userId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{groupId}")]
        public async Task<ActionResult> DeleteGroup([FromRoute] int schoolId, [FromRoute] int groupId)
        {
            var userId = User.GetUserId();
            await _groupService.DeleteGroupAsync(schoolId, groupId, userId);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "School-Admin")]
        [Route("{groupId}")]
        public async Task<ActionResult<ReturnGroupDto>> GetGroupForSchool([FromRoute]int schoolId,[FromRoute] int groupId)
        {
            var userId = User.GetUserId();
            var result = await _groupService.GetSchoolGroupByIdAsync(schoolId, groupId, userId);
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = "School-Admin")]
        public async Task<ActionResult<ReturnGroupDto>> GetAllSchoolGroups([FromRoute] int schoolId)
        {
            var userId = User.GetUserId();
            var result = await _groupService.GetAllSchoolGroupsAsync(schoolId, userId);
            return Ok(result);
        }
    }
}
