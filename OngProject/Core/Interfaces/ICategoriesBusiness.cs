using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICategoriesBusiness
    {
        Task GetAll();
        ResponseCategoriesDetailDto GetById(int id);
        Task Insert();
        Task Delete(int id);
        Task Update();
    }
}
