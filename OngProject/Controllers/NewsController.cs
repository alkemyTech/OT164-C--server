using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de Novedades de la ONG.
    /// </summary>
    [Route("/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsBusiness;

        public NewsController(INewsBusiness newsBusiness)
        {
            _newsBusiness = newsBusiness;
        }

        /// <summary>
        /// Obtiene una lista de novedades.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de novedades.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de actividades.</response>        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllNews([FromQuery] Filtros filtros)
        {
            var data = await _newsBusiness.GetAllNews(filtros);
            if(data != null)
            {
                return Ok(data);
            }

            return NoContent();
        }

        /// <summary>
        /// Obtiene una novedad por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene una Novedad por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NewsGetByIdDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NewsGetByIdDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetNewsById(int id)
        {

            Response<NewsGetByIdDTO> response = _newsBusiness.GetNewsById(id);


            if (!response.Succeeded)
            {


                return NotFound(response.Message);

            }


            return Ok(response.Data);

        }

        /// <summary>
        /// Obtiene los comentarios de una novedad por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene los comentarios de una novedad por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="204">NoContent. No se ha encontrado el objeto solicitado.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id:int}/comments")]
        public async Task<IActionResult> GetCommentsFromNew(int id)
        {
            var comentaries = await _newsBusiness.GetComementsFromNew(id);

            if (comentaries == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(comentaries);
            }
                
        }


        /// <summary>
        /// Crea una novedad en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un Json
        /// 
        /// Sample request:
        ///
        ///     {
        ///        "name": "Nueva novedad",
        ///        "content": "Contenido de nueva novedad",
        ///        "image": "www.example.com/imagen.png",
        ///        "categoriesId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="news">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpPost]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostNews(NewsDTO news)
        {
            Response<NewsDTO> response = await _newsBusiness.CreateNews(news);

            if (!response.Succeeded)
            {

                return NotFound(response);

            }

            return Ok(response);

        }


        /// <summary>
        /// Actualiza una novedad en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// 
        /// Sample request:
        ///
        ///     ID = 1
        ///     {
        ///        "name": "Novedad actualizada",
        ///        "content": "Contenido actualizado de novedad",
        ///        "image": "www.example.com/imagen.png",
        ///        "categoriesId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="newsDTO">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutNews(NewsDTO newsDTO, int id)
        {
            NewsDTO newsUpdate = await _newsBusiness.UpdateNews(newsDTO, id);
            if (newsUpdate != null)
            {
                return Ok(new Response<NewsDTO>
                {
                    Message = "Se actualizo correctamente la entidad",
                    Data = newsDTO,
                    Succeeded = true
                });
            }
            else
            {
                return NotFound(new Response<NewsDTO>
                {
                    Message = "No se modifico la entidad",
                    Data = newsDTO,
                    Succeeded = false,
                    Errors = new string[] { }
                });
            }
        }

        /// <summary>
        /// Elimina una novedad por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD una novedad por su Id especificada en la url
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var response = await _newsBusiness.DeleteNews(id);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
