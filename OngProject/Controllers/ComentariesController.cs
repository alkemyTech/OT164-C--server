using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de los Comentarios de la ONG.
    /// </summary>
    [Route("Comments")]
    [ApiController]
    public class ComentariesController : ControllerBase
    {
        private readonly IComentariesBusiness _comentariesBusiness;

        public ComentariesController(IComentariesBusiness comentariesBusiness)
        {
            _comentariesBusiness = comentariesBusiness;
        }

        /// <summary>
        /// Obtiene un listado de todos los comentarios.
        /// </summary>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Listado de comentarios.</response>        
        /// <response code="204">NoContent. No ha contenido que mostrar.</response>
        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public async Task<ActionResult<List<Comentaries>>> GetAll()
        {
            var data = await _comentariesBusiness.GetAll();
            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        /// <summary>
        /// Obtiene un comentario por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un comentario por su Id.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        /// <response code="200">OK. Devuelve el comentario por su Id.</response>
        /// <response code="404">NotFound. No se encuentra el contacto solicitado.</response>
        [Authorize(Roles = "1, 2")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comentaries>> GetById(int id)
        {
            


            Response<ComentariesByIdDTO> response = await _comentariesBusiness.GetById(id);

            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }

            return new JsonResult(response.Data) { StatusCode = 200 };


        }

        /// <summary>
        /// Actualiza un comentario en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// </remarks>
        /// <param name="updateComentaries">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Authorize(Roles = "1, 2")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(RequestUpdateComentariesDto updateComentaries, int id)
        {
            try
            {
                if (await _comentariesBusiness.Update(updateComentaries, id))
                {
                    Response<RequestUpdateComentariesDto> response = new Response<RequestUpdateComentariesDto>
                    {
                        Succeeded = true,
                        Message = "Se actualizo el comentario con id " + id,
                        Data = updateComentaries
                    };
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (UserException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        /// <summary>
        /// Crea una comentario en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un json.
        /// </remarks>
        /// <param name="comentariesDto">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [Authorize(Roles = "1, 2")]
        [HttpPost]
        public async Task<ActionResult> Post(RequestComentariesDto comentariesDto)
        {
            try
            {
                await _comentariesBusiness.Insert(comentariesDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }

        }

        /// <summary>
        /// Elimina un comentario por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD un comentario por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Response<ComentariesByIdDTO> response = await _comentariesBusiness.Delete(id);

            if (!response.Succeeded)
            {
                if (response.Errors[0].Equals("404"))
                {

                    return NotFound(response.Message);
                }

                if (response.Errors[0].Equals("403"))
                {

                    return Forbid(response.Message);
                }


            }

            return new JsonResult(response) { StatusCode = 200 };
        }


    }
}
