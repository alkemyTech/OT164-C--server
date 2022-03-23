using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseCategoriesDetailDto GetById(int id)
        {
           var query =  _unitOfWork.CategoriesRepository.GetById(id);
            if (query.Result == null)
            {
                return null;
            }
           return new ResponseCategoriesDetailDto
            { 
                CategoryId = query.Result.Id,
                Name = query.Result.Name,
                Description = query.Result.Description,
                Image = query.Result.Image
            };
            
        }

        public Task Insert()
        {
            throw new NotImplementedException();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
