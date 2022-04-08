﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
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
        EntityMapper mapper = new EntityMapper();

        public TestimonialsController(ITestimonialsBusiness testimonialsBusiness)
        {
            _testimonialsBusiness = testimonialsBusiness;
           
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<TestimonialsDTO>>>> GetAll([FromQuery] Filtros filtros)
        {
           

            var data = await _testimonialsBusiness.GetAll(filtros);
            
            return Ok(data);
        }

        [HttpGet("Id:int")]
        public async Task<ActionResult> GetById(int Id)
        {
            return NoContent();
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<TestimonialsDTO>>> Put(int id,[FromForm] TestimonialsPutDto testimonial)
        {
            return await _testimonialsBusiness.Update(id, testimonial);
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromForm]TestimonialsCreateDTO testimonial)
        {
            return await _testimonialsBusiness.Insert(testimonial);
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult> Delete(int Id)
        {
            return NoContent();
          
        }



    }
}
