using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class OrganizationsBusiness : IOrganizationsBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public OrganizationsBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task Delete(Organizations entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Organizations>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Organizations entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Organizations entity)
        {
            throw new NotImplementedException();
        }
    }
}
