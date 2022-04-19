using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    /// <summary>
    /// Web API para la gestion de testimonios de la ONG.
    /// </summary>
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



        // GET: /testimonials
        /// <summary>
        /// Obtiene una lista de  testimonios.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de testimonios.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de testimonios.</response>        
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponse<List<TestimonialsDTO>>>> GetAll([FromQuery] Filtros filtros)
        {
            var data = await _testimonialsBusiness.GetAll(filtros);
            return Ok(data);
        }


        // GET: /testimonials/5
        /// <summary>
        /// Obtiene un testimonio por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un testimonio por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        /// 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Id:int")]
        public async Task<ActionResult> GetById(int id)
        {
            Response<TestimonialsDTO> result = new Response<TestimonialsDTO>();
            var testimonials = await _testimonialsBusiness.GetById(id);
            if (testimonials != null)
            {
                result.Succeeded = true;
                return Ok(testimonials);

            }

            result.Succeeded = false;
            result.Message = $"There is no Slide with ID: {id}";
            return NotFound(result);
        }

        // PUT: /testimonials/5
        /// <summary>
        /// Actualiza un testimonio en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// 
        /// Sample request:
        ///
        ///     PUT /testimonials/5
        ///     {
        ///        "name": "nombre actualizado del testimonio",
        ///        "content": "contenido a actualizar",
        ///        "image": "imagen como string($binary)"
        ///     }
        ///
        /// </remarks>
        /// <param name="testimonial">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// 

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromForm] TestimonialsPutDto testimonial)
        {
            var result = await _testimonialsBusiness.Update(id, testimonial);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                if (result.Errors[1] == "404")
                {
                    return NotFound(result);
                }
                else
                {
                    return Problem(result.Message);
                }
            }
        }


        // POST: /testimonials
        /// <summary>
        /// Crea un testimonio en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un form.
        /// 
        /// Sample Request:
        /// 
        /// Form-Data
        /// 
        /// Name: Nombre del testimonio
        /// 
        /// Content: Descripcion del contenido del testimonio
        /// 
        /// Image: Imagen del contenido
        /// 
        /// </remarks>
        /// <param name="testimonial">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromForm] TestimonialsCreateDTO testimonial)
        {
            return await _testimonialsBusiness.Insert(testimonial);
        }



        // DELETE: /testimonials/5
        /// <summary>
        /// Elimina un testimonio por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD un testimonio por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        /// 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Response<TestimonialsDTO> response = await _testimonialsBusiness.Delete(id);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Message);
        }

    }

}

