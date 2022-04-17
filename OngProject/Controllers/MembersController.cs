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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de Miembros de la ONG.
    /// </summary>
    [Route("members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly IMemberBusiness _memberBusiness;
        private readonly string contenedor = "Members";

        public MembersController(IMemberBusiness memberBusiness, IFileManager fileManager)
        {
            _memberBusiness = memberBusiness;
            _fileManager = fileManager;
        }

        // GET: api/Members
        /// <summary>
        /// Obtiene una lista de  Miembros.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de Miembros.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de miembros.</response>        
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PagedResponse<List<MembersGetDTO>>>> GetAll([FromQuery] Filtros filtros)
        {
            var data = await _memberBusiness.GetAll(filtros);

            return Ok(data);
        }

        // GET: api/members/5
        /// <summary>
        /// Obtiene un miembro por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un miembro por su Id especificada en la url.
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
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(int id)
        {
            Response<MembersGetDTO> response = await _memberBusiness.GetById(id);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);

        }




        // PUT: /Members/5
        /// <summary>
        /// Actualiza un miembro en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// 
        /// Sample request:
        ///
        ///     PUT /Members/5
        ///     {
        ///        "Name": "nombre actualizado del miembro",
        ///        "FacebookUrl": "url a actualizar",
        ///        "Image": "imagen como string($binary)"
        ///        "InstagramUrl": "url a actualizar",
        ///        "LinkedinUrl": "url a actualizar",
        ///        "Description": "descripcion a actualizar"
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="updateMembersDto">Datos para actualizar en la BD.</param>
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
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(RequestUpdateMembersDto updateMembersDto, int id)
        {
            try
            {
                if (await _memberBusiness.Update(updateMembersDto, id))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        // POST: /Members
        /// <summary>
        /// Crea un miembro en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un form.
        /// 
        /// Sample Request:
        /// 
        /// Form-Data
        /// 
        /// Name: Nombre del miembro
        /// 
        /// FacebookUrl: Url del facebook del miembro
        /// InstagramUrl: Url del Instagram del miembro
        /// LinkedinUrl: Url del linkedin del miembro
        /// Description: Descripcion del miembro
        /// 
        /// Image: Boton para seleccionar imagen
        /// </remarks>
        /// <param name="member">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [Route("Members")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> Post([FromForm] MembersCreateDTO member)
        {
            string imagePath = "";
            try
            {
                var extension = Path.GetExtension(member.Image.FileName);
                imagePath = await _fileManager.UploadFileAsync(member.Image, extension, contenedor, member.Image.ContentType);
            }
            catch (Exception e)
            {
                return new Response<string>(null)
                {
                    Errors = new string[] { e.Message },
                    Succeeded = false,
                };
            }
            await _memberBusiness.Insert(member, imagePath);
            return new Response<string>("") { Message = "Created succesfully" };
        }

        // DELETE: api/members/5
        /// <summary>
        /// Elimina un miembro por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD un miembro por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            Response<MembersDTO> response = await _memberBusiness.Delete(id);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Message);
        }
    }

}
