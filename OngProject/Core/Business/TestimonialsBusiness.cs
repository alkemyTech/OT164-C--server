using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace OngProject.Core.Business
{
    public class TestimonialsBusiness: ITestimonialsBusiness
    {
        private readonly EntityMapper mapper = new EntityMapper();
        private readonly IUnitOfWork _unitOfWork;

        public TestimonialsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Delete(testimonials __testimonials)
        {
            throw new NotImplementedException();
        }

        public Task<List<testimonials>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<testimonials> GetById()
        {
            throw new NotImplementedException();
        }

        public async Task Insert(TestimonialsCreateDTO dto, string imagePath)
        {
            var t = mapper.TestimonialCreateDTOToTestimonial(dto, imagePath);
            await _unitOfWork.TestimonialsRepository.Insert(t);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task Update(testimonials __testimonials)
        {
            throw new NotImplementedException();
        }
    }
}
