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
        private readonly string contenedor = "Testimonials";

        public TestimonialsBusiness(IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
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
                Succeeded = true
            };
        }
    }
}
