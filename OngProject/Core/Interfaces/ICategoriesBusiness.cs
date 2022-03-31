using OngProject.Core.Mapper;
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
        Task<List<CategoriesGetDTO>> GetAll();
        ResponseCategoriesDetailDto GetById(int id);
        Task Insert();
        Task Delete(int id);
        Task<CategoriesGetDTO> Update(CategoriesUpdateDTO categoriesUpdateDTO, int id);
    }
}
