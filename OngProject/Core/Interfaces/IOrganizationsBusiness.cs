using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsBusiness
    {
        public Task<List<Organizations>> GetAll();
        public List<OrganizationsPublicDTO> GetPublic();
        Task<Response<OrganizationsUpdateDTO>> Update(OrganizationsUpdateDTO organizations, int id);
        public ResponseOrganizationsDetailDto GetById(int id);
        public Task Insert(Organizations entity);
        public Task Delete(Organizations entity);
    }
}
