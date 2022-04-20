using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de Roles de la ONG.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRolesBusiness _RoleBusiness;

        public RolesController(IRolesBusiness roleBusiness)
        {
            _RoleBusiness = roleBusiness;
        }

        /// <summary>
        /// Obtiene una lista de  roles.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de roles.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve una lista de roles.</response>        
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene un rol por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un rol por su Id especificada en la url.
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
        /// Crea una rol en la BD.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo objeto en la BD recibiendo los datos de un json.
        /// </remarks>
        /// <param name="activitiesCreationDTO">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [HttpPost]
        public async Task<ActionResult> Insert(Roles rol)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Elimina un rol por su Id.
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

        /// <summary>
        /// Actualiza un rol en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// </remarks>
        /// <param name="rol">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpPut]
        public async Task<ActionResult> Update(int id,Roles rol)
        {
            throw new NotImplementedException();
        }

    }
}
