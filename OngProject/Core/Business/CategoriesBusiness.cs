using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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
        private readonly EntityMapper mapper = new EntityMapper();

        public CategoriesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoriesGetDTO>> GetAll()
        {
            var data = await _unitOfWork.CategoriesRepository.GetAll();
            if(data != null)
            {
                return mapper.ToCagegoriesListDTO(data);

            }

            return null;

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
