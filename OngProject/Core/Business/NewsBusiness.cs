using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
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
        private readonly EntityMapper mapper = new EntityMapper();


        public NewsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateNews()
        {
            throw new NotImplementedException();
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

        public void UpdateNews()
        {
            throw new NotImplementedException();
        }
    }
}
