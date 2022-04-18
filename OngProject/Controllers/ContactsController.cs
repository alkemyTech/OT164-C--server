using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    /// Web API para la gestion de los Contactos de la ONG.
    /// </summary>
    [Route("Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsBusiness _contactsBusiness;

        public ContactsController(IContactsBusiness contactsBusiness)
        {
            _contactsBusiness = contactsBusiness;
        }

        // GET: /contacts
        /// <summary>
        /// Obtiene una lista de los contactos.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de los contactos.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        /// <response code="200">OK. Devuelve la lista de los contactos.</response> 
        /// <response code="403">Forbidden. No tiene permiso para realizar la peticion (rol incorrecto).</response> 
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Response<ContactsGetDTO>>), StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactsBusiness.GetAll();
            if (!result.Succeeded)
            {
                return Problem(result.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un contacto por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un contacto por su Id.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        /// <response code="200">OK. Devuelve el contacto por su Id.</response>
        /// <response code="404">NotFound. No se encuentra el contacto solicitado.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<ContactsGetDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _contactsBusiness.GetById(id);

            if (result.Succeeded)
                return Ok(result);
            else
                return NotFound(result);
        }

        /// <summary>
        /// Se agrega un nuevo contacto.
        /// </summary>
        /// <remarks>
        /// Se agrega un nuevo contacto.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        /// <response code="200">OK. Se registran los datos del contacto.</response>
        /// <response code="400">BadRequest. Se registran los datos del contacto.</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<ContactsGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Insert(ContactsGetDTO contact)
        {
            var result = await _contactsBusiness.Insert(contact);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Se actualiza un contacto con su Id.
        /// </summary>
        /// <remarks>
        /// Se actualiza un contacto con su Id.
        /// </remarks>
        /// <response code="200">OK. Se realiza la actualizacion del Contacto.</response>
        /// <response code="400">BadRequest. Alguno de los datos son requeridos.</response>
        /// <response code="404">NotFound. No se encuentra el Contacto con el Id ingresado.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response<ContactsGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, ContactsGetDTO contact)
        {
            var result = await _contactsBusiness.Update(contact,id);

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

        /// <summary>
        /// Se elimina un contacto con su Id.
        /// </summary>
        /// <remarks>
        /// Se elimina un contacto con su Id.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        /// <response code="200">OK. Se realiza el borrado del Contacto.</response>
        /// <response code="404">Not Found. No se encuentra el Contanto ingresado por Id.</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<ContactsGetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactsBusiness.Delete(id);
            if (result.Data == null)
            {
                return NotFound();
            }
            return Ok(result);
       
        }

    }
}
