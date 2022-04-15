using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de Usuarios de la ONG.
    /// </summary>
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;

        public UsersController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
        }

        // GET: /users
        /// <summary>
        /// Obtiene una lista de  usuarios.
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de usuarios.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response> 
        /// <response code="403">Forbidden. No tiene permiso para realizar la peticion (rol incorrecto).</response> 
        /// <response code="200">OK. Devuelve una lista de usuarios.</response>        
        [HttpGet]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _usersBusiness.GetAllAsync();
            return Ok(data);


        }
        


        // GET: /users/5
        /// <summary>
        /// Obtiene un usuario por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene un usuario por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Response<UserDTO> response = await _usersBusiness.GetById(id);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }


        // DELETE: /users/5
        /// <summary>
        /// Elimina un usuario por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de la BD un usuario por su Id especificada en la url. Realiza un SoftDelete, cambiando un tag a false.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            Response<ResponseUserDto> response = await _usersBusiness.Delete(id);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Message);
        }

        // PUT: /users/5
        /// <summary>
        /// Actualiza un usuario en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// 
        /// Sample request:
        ///
        ///     PUT /users/5
        ///     {
        ///        "firstName": "nombre a actualizar del usuario",
        ///        "lastName": "apellido a actualizar del usuario",
        ///        "password": "contraseña a actualizar"
        ///        "photo": "imagen como string($binary)"
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                Response<UserUpdateDTO> response = await _usersBusiness.Update(id, user);

                if (!response.Succeeded)
                {
                    return NotFound(response.Message);
                }

                return Ok(response.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }
    }
}
