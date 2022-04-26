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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager fileManager;
        private readonly EntityMapper mapper = new EntityMapper();
        private readonly HttpContext context;
        private readonly string contenedor = "Categories";

        public CategoriesBusiness(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            this.fileManager = fileManager;
            context = httpContextAccessor.HttpContext;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            Response<bool> respuesta = new Response<bool>();
            try
            {
                await _unitOfWork.CategoriesRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
               
                respuesta.Succeeded = true;
            }
            catch (Exception e)
            {
                respuesta.Succeeded = false;
                respuesta.Message = e.InnerException.Message;
            }
          
            return respuesta;
        }

        public async Task<Models.Response<PagedResponse<List<CategoriesGetDTO>>>> GetAll(Filtros filtros)
        {
            Models.Response<PagedResponse<List<CategoriesGetDTO>>> respuesta = new Models.Response<PagedResponse<List<CategoriesGetDTO>>>();
            try
            {
                var collection = await _unitOfWork.CategoriesRepository.GetAll() as IQueryable<Categories>;
                var pagedData = await _unitOfWork.CategoriesRepository.GetAllPaged(collection, filtros.Pagina, filtros.CantidadRegistrosPorPagina, filtros, context);
                PagedResponse<List<CategoriesGetDTO>> result = new PagedResponse<List<CategoriesGetDTO>>();
                result = mapper.PagedResponseCategoriesDTO(pagedData);
                respuesta.Data = result;
                respuesta.Succeeded = true;
            }
            catch (Exception e)
            {

                respuesta.Succeeded = false;
                respuesta.Message = e.InnerException.Message;
            }
           
            return respuesta;
        }

        public async Task<Response<ResponseCategoriesDetailDto>> GetById(int id)
        {
            Response<ResponseCategoriesDetailDto> respuesta = new Response<ResponseCategoriesDetailDto>();
            try
            {
                var query = await _unitOfWork.CategoriesRepository.GetById(id);
                if (query == null)
                {
                    respuesta.Succeeded = true;
                    return respuesta;
                }
                else
                {
                    respuesta.Succeeded = true;
                    ResponseCategoriesDetailDto resp = new ResponseCategoriesDetailDto();
                    resp = mapper.CategoriesToResponseCategoriesDetailDTO(query);
                    respuesta.Data = resp;
                }
            }
            catch (Exception e)
            {

                respuesta.Succeeded = false;
                respuesta.Message = e.InnerException.Message;
            }


            return respuesta;
            
        }

        public async Task<Response<CategoriesGetDTO>> Insert(CategorieCreationDTO categoriesCreationDto)
        {
            Response<CategoriesGetDTO> response = new Response<CategoriesGetDTO>();
            CategoriesGetDTO result = new CategoriesGetDTO();

            string imagePath = "";
            try
            {
                var extension = Path.GetExtension(categoriesCreationDto.Image.FileName);
                imagePath = await fileManager.UploadFileAsync(categoriesCreationDto.Image, extension, contenedor, categoriesCreationDto.Image.ContentType);
            }
            catch (Exception e)
            {
                response.Succeeded = false;
                response.Message = e.InnerException.Message;
                return response;
            }

            Categories categories = new Categories();
            categories = mapper.CategoriesCreationDTOToCategories(categoriesCreationDto);
            categories.Image = imagePath;

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

        public async Task<Response<CategoriesGetDTO>> Update(CategoriesUpdateDTO categoriesUpdateDTO, int id)
        {
            Response<CategoriesGetDTO> respuesta = new Response<CategoriesGetDTO>();
            Categories entityImage = new Categories();
            string imagePath = "";
            if(categoriesUpdateDTO.Image != null)
            {
               
                try
                {
                    var extension = Path.GetExtension(categoriesUpdateDTO.Image.FileName);
                    imagePath = await fileManager.UploadFileAsync(categoriesUpdateDTO.Image, extension, contenedor, categoriesUpdateDTO.Image.ContentType);
                    entityImage.Image = imagePath;
                }
                catch (Exception e)
                {
                    respuesta.Succeeded = false;
                    respuesta.Message = e.InnerException.Message;
                    return respuesta;
                }
            }
           

            try
            {
                Categories entity = mapper.ToCategories(categoriesUpdateDTO, id);

                if (string.IsNullOrEmpty(entityImage.Image))
                {
                    Categories categorieToImage = await _unitOfWork.CategoriesRepository.GetById(id);
                    entity.Image = categorieToImage.Image;
                }
                else
                {
                    entity.Image = entityImage.Image;
                }

                await _unitOfWork.CategoriesRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                CategoriesGetDTO dto = mapper.ToCategoriesDTO(entity);
                respuesta.Data = dto;
                respuesta.Succeeded = true;

            }
            catch (Exception e)
            {
                respuesta.Succeeded = false;
                respuesta.Message = e.Message;
                
            }
          

            return respuesta;
        }
    }
}
