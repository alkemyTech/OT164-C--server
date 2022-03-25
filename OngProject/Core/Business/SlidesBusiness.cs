using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.DataAccess;

using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class SlidesBusiness : ISlidesBusiness
    {

        private readonly IUnitOfWork<ApplicationDbContext> unitOfWork;

        public SlidesBusiness(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Delete(Slides slides)
        {
            throw new NotImplementedException();
        }

        public Task<List<Slides>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Slides> GetById()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Slides slides)
        {
            throw new NotImplementedException();
        }

        public Task Update(Slides slides)
        {
            throw new NotImplementedException();
        }
    }
}
