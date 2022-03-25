using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ActivitiesBusiness: IActivitiesBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public ActivitiesBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task Delete(Activities activities)
        {
            throw new NotImplementedException();
        }

        public Task<List<Activities>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Activities> GetById()
        {
            throw new NotImplementedException();
        }
        public Task Update(Activities activities)
        {
            throw new NotImplementedException();
        }
        
        public Task Insert(Activities activities)
        {
            throw new NotImplementedException();
        }
    }
}
