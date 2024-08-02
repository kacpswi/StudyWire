using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.DTOsModel.User;
using StudyWire.Application.Extensions;
using StudyWire.Application.Services.Interfaces;

namespace StudyWire.API.Controllers
{
    [ApiController]
    [Route("/api/schools")]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [Route("{schoolId}")]
        [Authorize(Roles = "School-Admin")]
        public async Task<ActionResult<ReturnSchoolDto>> GetSchoolById([FromRoute] int schoolId)
        {
            var result = await _schoolService.GetSchoolByIdAsync(schoolId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReturnSchoolDto>>> GetAllSchools()
        {
            var result = await _schoolService.GetSchoolsAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "School-Admin")]
        public async Task<ActionResult> PostNewSchool([FromBody] PostSchoolDto schoolDto)
        {
            int userId = User.GetUserId();
            var result = await _schoolService.CreateSchoolAsync(schoolDto, userId);
            Response.ExposeLocationHeader();
            return Created($"api/schools/{result}", null);
        }

        [HttpPut]
        [Route("{schoolId}")]
        [Authorize(Roles = "School-Admin")]
        public async Task<ActionResult<ReturnSchoolDto>> UpdateSchool([FromBody] PostSchoolDto schooltDto, [FromRoute] int schoolId)
        {
            int userId = User.GetUserId();
            var result = await _schoolService.UpdateSchoolAsync(schooltDto, userId, schoolId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{schoolId}")]
        [Authorize(Roles = "School-Admin")]
        public async Task<ActionResult> DeleteSchool([FromRoute]int schoolId)
        {
            int userId = User.GetUserId();
            await _schoolService.DeleteSchoolAsync(userId, schoolId);

            return NoContent();
        }

        [HttpGet]
        [Route("{schoolId}/members")]
        public async Task<ActionResult<IEnumerable<ReturnUserDto>>> GetSchoolMembers([FromRoute] int schoolId)
        {
            var members = await _schoolService.GetSchoolMembersAsync(schoolId);

            return Ok(members);
        }

        
        [HttpGet]
        [Route("{schoolId}/teachers")]
        [Authorize(Roles = "School-Admin, Admin")]
        public async Task<ActionResult<IEnumerable<ReturnUserDto>>> GetTeachersInSchool([FromRoute] int schoolId)
        {
            var teachers = await _schoolService.GetTeachersInSchoolAsync(schoolId);

            return Ok(teachers);
        }

        [HttpGet]
        [Route("{schoolId}/students")]
        [Authorize(Roles = "School-Admin, Admin")]
        public async Task<ActionResult<IEnumerable<ReturnUserDto>>> GetStudentsInSchool([FromRoute] int schoolId)
        {
            var strudents = await _schoolService.GetStudentsInSchoolAsync(schoolId);

            return Ok(strudents);
        }
    }
}
