using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.Extensions;
using StudyWire.Application.Helpers.Pagination;
using StudyWire.Application.Services.Interfaces;

namespace StudyWire.API.Controllers
{
    [ApiController]
    [Route("/api")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost]
        [Route("schools/{schoolId}/news")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<int>> PostNews([FromBody]PostNewsDto dto, int schoolId)
        {
            int userId = User.GetUserId();
            var newsId = await _newsService.CreateNewsAsync(dto, userId, schoolId);
            return Created($"api/schools/{schoolId}/news/{newsId}", null);
        }

        [HttpGet]
        [Route("schools/{schoolId}/news")]
        public async Task<ActionResult<IEnumerable<ReturnNewsDto>>> GetNews([FromQuery]PagedQuery query, [FromRoute] int schoolId)
        {
            var result = await _newsService.GetNewsBySchoolIdAsync(query, schoolId);
            Response.AddPaginationHeader(result);
            return Ok(result.Items);
        }

        [HttpGet]
        [Route("news")]
        public async Task<ActionResult<IEnumerable<ReturnNewsDto>>> GetNews([FromQuery] PagedQuery query)
        {
            var result = await _newsService.GetAllNewsAsync(query);
            Response.AddPaginationHeader(result);
            return Ok(result.Items);
        }

        [HttpPut]
        [Route("schools/{schoolId}/news/{newsId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<ReturnNewsDto>> UpdateNews([FromRoute]int schoolId, [FromRoute] int newsId, [FromBody]PostNewsDto dto)
        {
            int userId = User.GetUserId();
            var result = await _newsService.UpdateNewsAsync(dto, newsId, userId, schoolId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("schools/{schoolId}/news/{newsId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> DeleteNews([FromRoute] int schoolId, [FromRoute] int newsId)
        {
            int userId = User.GetUserId();
            await _newsService.DeleteNewsAsync(newsId, schoolId, userId);

            return NoContent();
        }

        [HttpGet]
        [Route("schools/{schoolId}/news/{newsId}")]
        public async Task<ActionResult<ReturnNewsDto>> GetNewsById([FromRoute] int schoolId, [FromRoute] int newsId)
        {
            var result = await _newsService.GetNewsByIdAsync(schoolId, newsId);
            return Ok(result);
        }
    }
}
