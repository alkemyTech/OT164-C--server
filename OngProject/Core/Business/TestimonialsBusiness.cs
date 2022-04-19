using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;




namespace OngProject.Core.Business
{
    public class TestimonialsBusiness: ITestimonialsBusiness
    {
        private readonly EntityMapper mapper = new EntityMapper();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly HttpContext context;
        private readonly string contenedor = "Testimonials";

        public TestimonialsBusiness(IUnitOfWork unitOfWork, IFileManager fileManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            context = httpContextAccessor.HttpContext;
   
        }

        public async Task<Response<TestimonialsDTO>> Delete(int id)
        {
            Response<TestimonialsDTO> response = new Response<TestimonialsDTO>();
            var testimonialId = await _unitOfWork.TestimonialsRepository.GetById(id);

            if (testimonialId == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no testimonial with ID: {id}";
                return response;
            }

            await _unitOfWork.TestimonialsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            response.Succeeded = true;
            response.Message = "Testimonial deleted successfully.";
            return response;
        }

        public async Task<PagedResponse<List<TestimonialsDTO>>> GetAll(Filtros filtros)
        {
            
            var collection = await _unitOfWork.TestimonialsRepository.GetAll() as IQueryable<testimonials>;
           
            var pagedData = await _unitOfWork.TestimonialsRepository.GetAllPaged(collection, filtros.Pagina, filtros.CantidadRegistrosPorPagina,filtros,context);
            PagedResponse<List<TestimonialsDTO>> result = new PagedResponse<List<TestimonialsDTO>>();
            result = mapper.PagedResponseTestimonialsDTO(pagedData);

            return result;
        }

        public async Task<TestimonialsDTO> GetById(int id)
        {
            var testimonial = await _unitOfWork.TestimonialsRepository.GetById(id);
            if (testimonial != null)
            {
                return mapper.TestimonialToTestimonialDTO(testimonial);
            }
            else
            {
                return null;
            }
        }

        public async Task<Response<string>> Insert(TestimonialsCreateDTO testimonial)
        {
            string imagePath = "";
            try
            {
                var extension = Path.GetExtension(testimonial.Image.FileName);
                imagePath = await _fileManager.UploadFileAsync(testimonial.Image, extension, contenedor, testimonial.Image.ContentType);
            }
            catch (Exception e)
            {
                return new Response<string>(null)
                {
                    Errors = new string[] { e.Message },
                    Succeeded = false,
                };
            }
            var t = mapper.TestimonialCreateDTOToTestimonial(testimonial, imagePath);
            await _unitOfWork.TestimonialsRepository.Insert(t);
            await _unitOfWork.SaveChangesAsync();
            return new Response<string>("") { Message = "Created succesfully" };
        }

        public async Task<Response<TestimonialsDTO>> Update(int id,TestimonialsPutDto testimonial)
        {

            var t = await _unitOfWork.TestimonialsRepository.GetById(id);
            if (t == null)
            {
                return new Response<TestimonialsDTO>
                {
                    Errors = new string[] { $"Testimonial id {id} does not exist" },
                    Succeeded = false
                };
            }

            string imagePath = "";
            if (testimonial.Image != null)
            {
                try
                {
                    var extension = Path.GetExtension(testimonial.Image.FileName);
                    imagePath = await _fileManager.UploadFileAsync(testimonial.Image, extension, contenedor, testimonial.Image.ContentType);
                }
                catch (Exception e)
                {
                    return new Response<TestimonialsDTO>(null)
                    {
                        Errors = new string[] { e.Message },
                        Succeeded = false,
                    };
                }
            }
            if (String.IsNullOrEmpty(imagePath))
                imagePath = t.image;

            await _unitOfWork.TestimonialsRepository.Update(mapper.TestimonialPutDTOToTestimonial(id, testimonial, imagePath));
            await _unitOfWork.SaveChangesAsync();

            t = await _unitOfWork.TestimonialsRepository.GetById(id);
            var responsedto = mapper.TestimonialToTestimonialDTO(t);
            return new Response<TestimonialsDTO>()
            {
                Data = responsedto,
                Succeeded = true,
                Message = "Updated Successfully"
            };
        }
    }
}
