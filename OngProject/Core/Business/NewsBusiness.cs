using Microsoft.EntityFrameworkCore;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();
       



        public NewsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //   _dbSet = _context.Set<News>();
        }

        public async Task<Response<NewsDTO>> CreateNews(NewsDTO news)
        {
            Response<NewsDTO> response = new Response<NewsDTO>();

            if(news.Name.Equals(string.Empty))
            {
                response.Succeeded = false;
                response.Message = "Name is required";
                return response;
            }

            if (news.Content.Equals(string.Empty))
            {
                response.Succeeded = false;
                response.Message = "Content is required";
                return response;
            }

            News NewToInsert = mapper.NewsDTOToNewsForInsert(news);

            response.Data = news;
            response.Succeeded = true;
            response.Message = "News created successfully";
            await _unitOfWork.NewsRepository.Insert(NewToInsert);
            await _unitOfWork.SaveChangesAsync();
            return response;


        }

        public void DeleteNews()
        {
            throw new NotImplementedException();
        }

        public void GetAllNews()
        {
            //_unitOfWork.NewsRepository.GetAll();
            throw new NotImplementedException();
        }

        public Response<NewsGetByIdDTO> GetNewsById(int id)
        {
            Response<NewsGetByIdDTO> response = new Response<NewsGetByIdDTO>();

            var query =  _unitOfWork.NewsRepository.GetByIdIncludeAsync(id, "Categories");
            
            if (query.Result == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no news with ID: {id}";

                return response;
            }

           NewsGetByIdDTO data  = mapper.ToNewsByIdDTO(query);

           

            response.Data = data;
            response.Succeeded = true;
            return response;

        }

        public async Task<NewsDTO> UpdateNews(NewsDTO newsDTO, int id)
        {
            News news = mapper.ToNews(newsDTO, id);
            if (_unitOfWork.NewsRepository.EntityExist(id) == true)
            {
                await _unitOfWork.NewsRepository.Update(news);
                await _unitOfWork.SaveChangesAsync();
                NewsDTO dto = mapper.ToNewsDTO(news);
                return dto;
            }
            else
            {
                return null;
            }
        }
    }


}

