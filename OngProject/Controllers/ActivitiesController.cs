using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OngProject.Core.Helper;
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
    /// <summary>
    /// Web API para la gestion de Actividades de la ONG.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]  
    public class ActivitiesController: ControllerBase
    {
        private readonly IActivitiesBusiness activities;
        private readonly IFileManager _fileManager;
        private EntityMapper mapper = new EntityMapper();
        private readonly string contenedor = "Activities";
      

        public ActivitiesController(IActivitiesBusiness activities, IFileManager fileManager)
        {
            this.activities = activities;
            this._fileManager = fileManager;
        }

        // GET: api/Activities
        /// <summary>
        /// Obtiene una lista de  actividades.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de actividades.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de actividades.</response>        
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        // GET: api/Activities/5
        /// <summary>
        /// Obtiene una actividad por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene una actividad por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }


        // POST: /Activities
        /// <summary>
        /// Crea una actividad en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un form.
        /// 
        /// Sample Request:
        /// 
        /// Form-Data
        /// 
        /// Name: Nombre de la actividad
        /// 
        /// Content: Descripcion del contenido de la actividad
        /// 
        /// Image: Boton para seleccionar imagen
        /// </remarks>
        /// <param name="activitiesCreationDTO">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [Route("Activities")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ActivitiesGetDto>>> Insert([FromForm]ActivitiesCreationDTO activitiesCreationDTO)
        {
            Response<ActivitiesGetDto> result = new Response<ActivitiesGetDto>();
          
            if (ModelState.IsValid)
            {
                var activity = mapper.ActivityCreationDTOToActivity(activitiesCreationDTO);
                
                Tools tools = new Tools(activitiesCreationDTO.Image, contenedor, _fileManager);
                Response<string> imageResult = await tools.EvaluateImage();
                if (imageResult.Succeeded)
                {
                    activity.Image = imageResult.Data;
                }
              
                result = await activities.Insert(activity);

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


        // PUT: /Activities/5
        /// <summary>
        /// Actualiza una actividad en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// 
        /// Sample request:
        ///
        ///     PUT /Activities/5
        ///     {
        ///        "name": "nombre actualizado de la actividad",
        ///        "content": "contenido a actualizar",
        ///        "image": "imagen como string($binary)"
        ///     }
        ///
        /// </remarks>
        /// <param name="activitiesDTO">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutNews(ActivitiesDTO activitiesDTO, int id)
        {
            ActivitiesDTO activitiesUdate = await activities.Update(activitiesDTO, id);
            if (activitiesUdate != null)
            {
                return Ok(new Response<ActivitiesDTO>
                {
                    Message = "Se actualizo correctamente la entidad",
                    Data = activitiesDTO,
                    Succeeded = true
                });
            }
            else
            {
                return NotFound(new Response<ActivitiesDTO>
                {
                    Message = "No se modifico la entidad",
                    Data = activitiesDTO,
                    Succeeded = false,
                    Errors = new string[] { }
                });
            }
        }


        // DELETE: api/Activities/5
        /// <summary>
        /// Elimina una actividad por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD una actividad por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
