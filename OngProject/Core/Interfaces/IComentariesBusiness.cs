using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IComentariesBusiness
    {
        Task<List<ComentariesGetDTO>> GetAll();
        Task GetById(int id);
        Task Insert();
        Task Delete(int id);
        Task Update();
    }
}
