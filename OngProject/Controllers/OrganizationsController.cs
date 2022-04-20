using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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
    /// Web API para la gestion de Organizaciones de la ONG.
    /// </summary>
    [Route("organizations")]
    [ApiController]

    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _organizationsBusiness;
        private readonly EntityMapper mapper = new EntityMapper();

        public OrganizationsController(IOrganizationsBusiness organizationsBusiness)
        {
            _organizationsBusiness = organizationsBusiness;
        }

        /// <summary>
        /// Obtiene una lista de las organizaciones.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de las organizaciones.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de organizaciones.</response>        
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("public")]
        [ProducesResponseType(typeof(List<OrganizationsPublicDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<OrganizationsPublicDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPublic()
        {

            List<OrganizationsPublicDTO> result = new List<OrganizationsPublicDTO>();
            result = _organizationsBusiness.GetPublic();

            if (result != null)
            {
                return Ok(result);
            }

            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Actualiza una organizacion en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.     
        /// </remarks>
        /// <param name="organizationUpdateDTO">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("public/{id:int}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> PutOrganization(int id, OrganizationsUpdateDTO organizationUpdateDTO)
        {
            Response<OrganizationsUpdateDTO> response = new Response<OrganizationsUpdateDTO>();

            var result = _organizationsBusiness.GetById(id);
            if (result != null)
            {
                response = await _organizationsBusiness.Update(organizationUpdateDTO, id);
                if (response.Succeeded)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
                
            }
            else
            {
                return NotFound(response);
            }
        }

        /// <summary>
        /// Obtiene una organizacion por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene una organizacion por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Crea una organizacion en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un json.
        /// </remarks>
        /// <param name="organizationsDto">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [HttpPost]
        public async Task<ActionResult> Insert(Organizations organizationsDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina una organizacion por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD una rol por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
