using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpContext context;
        private readonly EntityMapper mapper = new EntityMapper();
       



        public NewsBusiness(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            context = httpContextAccessor.HttpContext;
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

        public async Task<Response<string>> DeleteNews(int id)
        {
            var news = await _unitOfWork.NewsRepository.GetById(id);
            if(news == null)
            {
                return new Response<string>()
                {
                    Errors = new string[] { $"News id {id} does not exist" },
                    Succeeded = false
                };
            }
            await _unitOfWork.NewsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return new Response<string>()
            {
                Succeeded = true,
                Message = "News deleted successfully"
            };
        }

        public async Task<PagedResponse<List<NewsDTO>>> GetAllNews(Filtros filtros)
        {
            var collection = await _unitOfWork.NewsRepository.GetAll() as IQueryable<News>;

            var pagedData = await _unitOfWork.NewsRepository.GetAllPaged(collection, filtros.Pagina, filtros.CantidadRegistrosPorPagina, filtros, context);
            PagedResponse<List<NewsDTO>> result = new PagedResponse<List<NewsDTO>>();
            result = mapper.PagedResponseNewsDTO(pagedData);
            return result;
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

