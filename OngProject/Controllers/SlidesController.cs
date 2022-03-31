using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Repositories.Interfaces;
using OngProject.Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlidesBusiness _slides;

        public SlidesController(ISlidesBusiness slides)
        {
            _slides = slides;
        }

        // GET: api/Slides
        [HttpGet]
        public async Task<ActionResult<List<SlidesDTO>>> GetSlides()
        {
            var data = await _slides.GetAll();
            return Ok(data);
        }

        // GET: api/Slides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlidesDTO>> GetSlides(int id)
        {
            var slide = await _slides.GetById(id);
            return Ok(slide);
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutSlides(int id, [FromForm] SlidesDTO slidesDTO)
        {
            try
            {
                if (slidesDTO == null)
                    return BadRequest("Rellene todos los campos para continuar");

               var slideDB = await _slides.GetById(id);

                if (slideDB == null)
                    return NotFound();


               await _slides.Update(slidesDTO, id);

               return NoContent();

            }
            catch (Exception e)
            {
                return BadRequest(error: e);
            }
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
