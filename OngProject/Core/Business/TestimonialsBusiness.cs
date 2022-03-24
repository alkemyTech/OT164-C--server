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
    public class TestimonialsBusiness:ITestimonialsBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public TestimonialsBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task Delete(testimonials __testimonials)
        {
            return null;
        }

        public Task<List<Activities>> GetAll()
        {
            return null;
        }

        public Task<Activities> GetById()
        {
            return null;
        }
        public Task Update(testimonials __testimonials)
        {
            return null;
        }

        public Task Insert(testimonials __testimonials)
        {
            return null;
        }

        Task<List<testimonials>> ITestimonialsBusiness.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<testimonials> ITestimonialsBusiness.GetById()
        {
            throw new NotImplementedException();
        }
    }
}
