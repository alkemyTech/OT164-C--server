using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlidesBusiness slides;

        public SlidesController(ISlidesBusiness __slides)
        {
           this. slides = __slides;
        }

        // GET: api/Slides
        [HttpGet]
          public async Task<ActionResult<IEnumerable<SlidesGetDTO>>> GetSlides()
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    return await slides.GetAll();

                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        // GET: api/Slides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slides>> GetSlides(int id)
        {
           

            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlides(int id, Slides slides)
        {
            return null;
        }

       
        [HttpPost]
        public async Task<ActionResult<Slides>> PostSlides(Slides slides)
        {
            return null;
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlides(int id)
        {



            return null;
        }

        private bool SlidesExists(int id)
        {
            return true;
        }
    }
}
