using OngProject.Core.Helper;
using OngProject.Core.Models;
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
        Task<PagedResponse<List<MembersGetDTO>>> GetAll(Filtros filtros);
        Task<Response<MembersGetDTO>> GetById(int id);
        Task Insert(MembersCreateDTO members, string imagePath);
        public Task<Response<MembersDTO>> Delete(int id);
        Task<bool> Update(RequestUpdateMembersDto updateMembersDto, int id);
    }
}
