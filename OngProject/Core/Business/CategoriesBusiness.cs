using Microsoft.AspNetCore.Http;
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
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();
        private readonly HttpContext context;

        public CategoriesBusiness(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            context = httpContextAccessor.HttpContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<CategoriesGetDTO>>> GetAll(Filtros filtros)
        {

            var collection = await _unitOfWork.CategoriesRepository.GetAll() as IQueryable<Categories>;

            var pagedData = await _unitOfWork.CategoriesRepository.GetAllPaged(collection, filtros.Pagina, filtros.CantidadRegistrosPorPagina, filtros, context);
            PagedResponse<List<CategoriesGetDTO>> result = new PagedResponse<List<CategoriesGetDTO>>();
            result = mapper.PagedResponseCategoriesDTO(pagedData);

            return result;
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

        public async Task<Response<CategoriesGetDTO>> Insert(Categories categories)
        {
            Response<CategoriesGetDTO> response = new Response<CategoriesGetDTO>();
            CategoriesGetDTO result = new CategoriesGetDTO();
            try
            {
                await _unitOfWork.CategoriesRepository.Insert(categories);
                await _unitOfWork.SaveChangesAsync();
                result = mapper.ToCategoriesDTO(categories);
                response.Succeeded = true;
                response.Data = result;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Errors = new[] { e.InnerException.Message };
                response.Succeeded = false;
            }
            return response;
        }

        public async Task<CategoriesGetDTO> Update(CategoriesUpdateDTO categoriesUpdateDTO, int id)
        {
            Categories entity = mapper.ToCategories(categoriesUpdateDTO, id);

            await _unitOfWork.CategoriesRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            CategoriesGetDTO dto = mapper.ToCategoriesDTO(entity);

            return dto;
        }
    }
}
