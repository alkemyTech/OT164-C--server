using OngProject.Core.Helper;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICategoriesBusiness
    {
        Task<Models.Response<PagedResponse<List<CategoriesGetDTO>>>> GetAll(Filtros filtros);
        Task<Response<ResponseCategoriesDetailDto>> GetById(int id);
        Task<Response<CategoriesGetDTO>> Insert(CategorieCreationDTO categories);
        Task<Response<bool>> Delete(int id);
        Task<Response<CategoriesGetDTO>> Update(CategoriesUpdateDTO categoriesUpdateDTO, int id);
    }
}
