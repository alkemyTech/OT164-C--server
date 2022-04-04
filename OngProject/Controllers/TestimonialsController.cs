using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("testimonials")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly ITestimonialsBusiness _testimonialsBusiness;
        private readonly string contenedor = "Testimonials";

        public TestimonialsController(ITestimonialsBusiness testimonialsBusiness, IFileManager fileManager)
        {
            _testimonialsBusiness = testimonialsBusiness;
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return NoContent();
        }

        [HttpGet("Id:int")]
        public async Task<ActionResult> GetById(int Id)
        {
            return NoContent();
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromForm]TestimonialsCreateDTO testmimonial)
        {
            string imagePath = "";
            try
            {
                var extension = Path.GetExtension(testmimonial.Image.FileName);
                imagePath = await _fileManager.UploadFileAsync(testmimonial.Image, extension, contenedor,testmimonial.Image.ContentType);
            }
            catch (Exception e)
            {
                return new Response<string>(null)
                {
                    Errors = new string[] { e.Message },
                    Succeeded = false,
                };
            }
            await _testimonialsBusiness.Insert(testmimonial, imagePath);
            return new Response<string>("") { Message = "Created succesfully" };
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult> Delete(int Id)
        {
            return NoContent();
          
        }


    }
}
