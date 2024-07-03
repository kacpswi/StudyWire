using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyWire.Application.DTOsModel.News;
using StudyWire.Application.Exceptions;
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
            if (user.SchoolId != schoolId) throw new BadRequestException("Cannot post other schools' newses");

            var news = _mapper.Map<News>(newsDto);
            news.Author = user.Name + " " + user.Surename;
            news.SchoolId = (int)user.SchoolId;

            await _newsRepository.AddNews(news);

            if (!await _newsRepository.Save()) throw new BadRequestException("Failed to post news");

            return news.Id;
        }

        public async Task DeleteNewsAsync(int newsId, int schoolId, int userId)
        {
            var news = await _newsRepository.GetNewsByIdAsync(newsId);
            if (news == null) throw new NotFoundException("News not found");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user.SchoolId != news.SchoolId || schoolId != user.SchoolId)
                throw new BadRequestException("Cannot delete other schools' newses");

            await _newsRepository.DeleteNewsAsync(news);
            if(!await  _newsRepository.Save()) throw new BadRequestException("Unable to delete news");

        }

        public async Task<IEnumerable<ReturnNewsDto>> GetAllNewsAsync()
        {
            var newses = await _newsRepository.GetAllNewsAsync();
            var dtos = _mapper.Map<IEnumerable<ReturnNewsDto>>(newses);
            return dtos;
        }

        public async Task<ReturnNewsDto> GetNewsByIdAsync(int id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);
            if (news == null) throw new NotFoundException("News not found");

            return _mapper.Map<ReturnNewsDto>(news);
        }

        public async Task<IEnumerable<ReturnNewsDto>> GetNewsBySchoolIdAsync(int schoolId)
        {
            var newses = await _newsRepository.GetAllNewsBySchoolIdAsync(schoolId);
            var result = _mapper.Map<IEnumerable<ReturnNewsDto>>(newses);
            return result;
        }

        public async Task<ReturnNewsDto> UpdateNewsAsync(PostNewsDto newsDto, int newsId, int userId, int schoolId)
        {
            var news = await _newsRepository.GetNewsByIdAsync(newsId);
            if (news != null)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user.SchoolId != news.SchoolId || schoolId != user.SchoolId)
                    throw new BadRequestException("Cannot edit other schools' newses");

                var newNews = _mapper.Map(newsDto, news);

                if (!await _newsRepository.Save())
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
