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
    public class RolesBusiness : IRolesBusiness
    {

        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public RoleBusiness(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }


        public Task Insert(Roles rol)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

      
    

        public Task Update(Roles rol)
        {
            throw new NotImplementedException();
        }
    }
}
