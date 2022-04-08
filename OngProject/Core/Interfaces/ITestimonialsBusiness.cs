using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBusiness
    {
        public Task<PagedResponse<List<TestimonialsDTO>>> GetAll(Filtros filtros);

        public Task<testimonials> GetById();
        public Task<Response<string>> Insert(TestimonialsCreateDTO dto);
        public Task<Response<TestimonialsDTO>> Update(int id, TestimonialsPutDto testmimonial);
        public Task<Response<TestimonialsDTO>> Delete(int id);
    }    
}
