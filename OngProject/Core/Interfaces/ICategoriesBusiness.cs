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
        Task<PagedResponse<List<CategoriesGetDTO>>> GetAll(Filtros filtros);
        ResponseCategoriesDetailDto GetById(int id);
        Task<Response<CategoriesGetDTO>> Insert(Categories categories);
        Task Delete(int id);
        Task<CategoriesGetDTO> Update(CategoriesUpdateDTO categoriesUpdateDTO, int id);
    }
}
