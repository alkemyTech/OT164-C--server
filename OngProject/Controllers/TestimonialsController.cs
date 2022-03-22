using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Data;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsBusiness testimonialsBusiness;

        public TestimonialsController(ITestimonialsBusiness __testimonials)
        {
            testimonialsBusiness=__testimonials;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return null;
        }



        [HttpPost]
        public async Task<ActionResult> Insert(testimonials __testimonials)
        {
            return null;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return null;
        }


        [HttpPut]
        public async Task<ActionResult> Update(testimonials __testimonials)
        {
            return null;
        }

    }
}

  
