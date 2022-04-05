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
using OngProject.Core.Models;
using System.IO;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlidesBusiness _slides;
        private EntityMapper mapper = new EntityMapper();
        private readonly IFileManager _fileManager;

        public SlidesController(ISlidesBusiness slides,IFileManager fileManager)
        {
            _slides = slides;
            _fileManager = fileManager;
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


      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      //  [Authorize(Roles = "1")]
        [Route("Slides")]
        [HttpPost]
        public async Task<ActionResult<Response<SlidesDTO>>> Insert(SlidesDTO slidesDTO)
        {
            Response<SlidesDTO> result = new Response<SlidesDTO>();
            if (ModelState.IsValid)
            {
            
                    try
                    {
                        
                    result = await _slides.Insert(slidesDTO);
                    result.Succeeded = true;
                    result.Data = slidesDTO;
                    result.Errors = null;
                    result.Message = "Se agrego corectamente el slide";
                    }
                    catch (Exception e)
                    {
                        result.Message = e.Message;
                        result.Succeeded = false;
                        return BadRequest(result);

                    }


                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }

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
