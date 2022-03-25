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
        public Task GetById(int id);
        public Task Insert(Organizations entity);
        public Task Update(Organizations entity);
        public Task Delete(Organizations entity);
    }
}
