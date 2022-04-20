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

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de las Presentaciones de la ONG.
    /// </summary>
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
        /// <summary>
        /// Obtiene una lista de todos las presentaciones.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de todos las presentaciones.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de de todos las presentaciones.</response>        
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<List<SlidesDTO>>> GetSlides()
        {
            var data = await _slides.GetAll();
            return Ok(data);
        }

        /// <summary>
        /// Obtiene una presentacion por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene una presentacion por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<SlidesDTO>>> GetSlides(int id)
        {
            Response<SlidesDTO> result = new Response<SlidesDTO>();
            var slide = await _slides.GetById(id);
            if(slide != null)
            {
                result.Data = slide;
                result.Succeeded = true;
                return Ok(slide);

            }

            result.Succeeded = false;
            result.Message = $"There is no Slide with ID: {id}";
            return NotFound(result);
            
        }

        /// <summary>
        /// Actualiza una presentacion en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Form, y buscando el objeto por su id.
        /// </remarks>
        /// <param name="slidesDTO">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
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


        /// <summary>
        /// Crea una presentacion en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un json.
        /// </remarks>
        /// <param name="slidesDTO">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
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

        /// <summary>
        /// Elimina una presentacion por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD una presentacion por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<int>>> DeleteSlides(int id)
        {
            Response<int> result = new Response<int>();
            result = await _slides.Delete(id);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        private bool SlidesExists(int id)
        {
            return true;
        }
    }
}
