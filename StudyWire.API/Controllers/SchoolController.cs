using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.DTOsModel.School;
using StudyWire.Application.Extensions;
using StudyWire.Application.Services.Interfaces;

namespace StudyWire.API.Controllers
{
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
        public async Task<ActionResult<ReturnSchoolDto>> GetSchoolById([FromRoute] int schoolId)
        {
            var result = await _schoolService.GetSchoolByIdAsync(schoolId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnSchoolDto>>> GetAllSchools()
        {
            var result = await _schoolService.GetSchoolsAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> PostNewSchool([FromBody] PostSchoolDto schoolDto)
        {
            int userId = User.GetUserId();
            var result = await _schoolService.CreateSchoolAsync(schoolDto, userId);
            return Created($"api/schools/{result}", null);
        }

        [HttpPut]
        [Route("{schoolId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<ReturnSchoolDto>> UpdateSchool([FromBody] PostSchoolDto schooltDto, [FromRoute] int schoolId)
        {
            int userId = User.GetUserId();
            var result = await _schoolService.UpdateSchoolAsync(schooltDto, userId, schoolId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{schoolId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> DeleteSchool([FromRoute]int schoolId)
        {
            int userId = User.GetUserId();
            await _schoolService.DeleteSchoolAsync(userId, schoolId);

            return NoContent();
        }



    }
}
