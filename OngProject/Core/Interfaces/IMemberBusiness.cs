using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness
    {
        Task<List<MembersGetDTO>> GetAll();
        Task GetById(int id);
        Task Insert(MembersCreateDTO members, string imagePath);
        Task Delete(int id);
        Task Update();
    }
}
