using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.Exceptions;
using StudyWire.Application.Helpers.Pagination;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Domain.Entities;
using StudyWire.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly IMapper _mapper;
        private readonly INewsRepository _newsRepository;
        private readonly UserManager<AppUser> _userManager;

        public NewsService(IMapper mapper, INewsRepository newsRepository, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
            _userManager = userManager;
        }
        public async Task<int> CreateNewsAsync(PostNewsDto newsDto, int userId, int schoolId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User not found");

            if (user.SchoolId != schoolId) throw new BadRequestException("Cannot post other schools' news");

            var news = _mapper.Map<News>(newsDto);
            news.Author = user.Name + " " + user.Surename;
            news.SchoolId = (int)user.SchoolId;
            news.CreatedById = userId;

            await _newsRepository.AddNewsAsync(news);

            if (!await _newsRepository.SaveAsync()) throw new BadRequestException("Failed to post news");

            return news.Id;
        }

        public async Task DeleteNewsAsync(int newsId, int schoolId, int userId)
        {
            var news = await _newsRepository.GetNewsByIdAsync(newsId);
            if (news == null) throw new NotFoundException("News not found");

            if (userId != news.CreatedById)
                throw new BadRequestException("Cannot delete others news");

            _newsRepository.DeleteNews(news);
            if(!await  _newsRepository.SaveAsync()) throw new BadRequestException("Unable to delete news");

        }

        public async Task<PagedResult<ReturnNewsDto>> GetAllNewsAsync(PagedQuery query)
        {
            var paginationResult = await _newsRepository.GetAllNewsAsync(query.SearchPhrase, query.PageSize, query.PageNumber, query.SortBy, query.SortDirection);
            var dtos = _mapper.Map<IEnumerable<ReturnNewsDto>>(paginationResult.Item1);

            var result = new PagedResult<ReturnNewsDto>(dtos, paginationResult.Item2, query.PageSize, query.PageNumber);
            return result;
        }

        public async Task<ReturnNewsDto> GetNewsByIdAsync(int schoolId, int newsId)
        {

            var news = await _newsRepository.GetNewsByIdAsync(newsId);
            if (news == null) throw new NotFoundException("News not found");

            if (news.SchoolId != schoolId) throw new NotFoundException("School not found");

            return _mapper.Map<ReturnNewsDto>(news);
        }

        public async Task<PagedResult<ReturnNewsDto>> GetNewsBySchoolIdAsync(PagedQuery query, int schoolId)
        {
            var paginationResult = await _newsRepository.GetAllNewsBySchoolIdAsync(query.SearchPhrase, query.PageSize, query.PageNumber, query.SortBy, query.SortDirection, schoolId);
            var dtos = _mapper.Map<IEnumerable<ReturnNewsDto>>(paginationResult.Item1);

            var result = new PagedResult<ReturnNewsDto>(dtos, paginationResult.Item2, query.PageSize, query.PageNumber);
            return result;
        }

        public async Task<ReturnNewsDto> UpdateNewsAsync(PostNewsDto newsDto, int newsId, int userId, int schoolId)
        {
            var news = await _newsRepository.GetNewsByIdAsync(newsId);
            if (news != null)
            {
                if (userId != news.CreatedById)
                    throw new BadRequestException("Cannot edit others news");

                var newNews = _mapper.Map(newsDto, news);

                if (!await _newsRepository.SaveAsync())
                    throw new BadRequestException("Unable to update news");

                var newNewsDto = _mapper.Map<ReturnNewsDto>(newNews);
                return newNewsDto;
            }
            else
            {
                throw new NotFoundException("News not found");
            }


        }
    }
}
