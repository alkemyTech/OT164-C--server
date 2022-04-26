using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OngProject.Core.Models;
using OngProject.Core.Helper;

namespace OngProject.Controllers
{
    /// <summary>
    /// Web API para la gestion de Categorias de la ONG.
    /// </summary>
    [Route("categories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBusiness _categoriesBusiness;
        private EntityMapper mapper = new EntityMapper();

        public CategoriesController(ICategoriesBusiness categoriesBusiness)
        {
            _categoriesBusiness = categoriesBusiness;
        }

        // GET: /Categories
        /// <summary>
        /// Obtiene un listado de todas las categorias.
        /// </summary>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Listado de categorias.</response>        
        /// <response code="500">Internal Server Error. Ha ocurrido un error interno en el servidor y no se pudo llevar a cabo la peticion.</response>

        [HttpGet]
        [Authorize(Roles = "1")]
        [ProducesResponseType(typeof(Response<PagedResponse<List<CategoriesGetDTO>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<PagedResponse<List<CategoriesGetDTO>>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] Filtros filtros)
        {
            var result = await _categoriesBusiness.GetAll(filtros);
            if (!result.Succeeded)
            {
                return StatusCode(500, result);
            }
            return Ok(result);
        }

        // GET: /Categories/5
        /// <summary>
        /// Obtiene una categoria por su Id.
        /// </summary>
        /// <remarks>
        /// Obtiene una categoria por su Id especificada en la url.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="500">Internal Server Error. Ha ocurrido un error interno en el servidor y no se pudo llevar a cabo la peticion.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(typeof(Response<ResponseCategoriesDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponseCategoriesDetailDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<ResponseCategoriesDetailDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var result = await _categoriesBusiness.GetById(id);
            if (!result.Succeeded)
            {
                return StatusCode(500, result);
            }
            else
            {
                if(result.Data == null)
                {
                    return NotFound();
                }
            }

            return Ok(result);

        }

        // PUT: /Categories/5
        /// <summary>
        /// Actualiza una categoria en la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza un nuevo objeto en la BD recibiendo los datos de un Json, y buscando el objeto por su id.
        /// 
        /// Sample request:
        ///
        /// Name: Nuevo nombre para la categoria.
        /// 
        /// Description: Nueva descripcion para la categoria.
        /// 
        /// Image: Nueva imagen para la categoria
        ///
        /// </remarks>
        /// <param name="categoriesUpdateDTO">Datos para actualizar en la BD.</param>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente actualizado en la BD.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="500">Internal Server Error. Ha ocurrido un error interno en el servidor y no se pudo llevar a cabo la peticion.</response>
        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Response<CategoriesGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CategoriesGetDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<CategoriesGetDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCategories(int id, [FromForm]CategoriesUpdateDTO categoriesUpdateDTO)
        {
            var result = _categoriesBusiness.GetById(id);
            if (result != null)
            {
                var response = await _categoriesBusiness.Update(categoriesUpdateDTO, id);

                if (!response.Succeeded)
                {
                    return StatusCode(500, response);
                }
                return Ok(response);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: /Categories
        /// <summary>
        /// Crea una nueva categoria en la BD.
        /// </summary>
        /// <remarks>
        ///Sample Request:
        ///
        /// Name: Nombre de la nueva categoria.
        /// 
        /// Descripcion: Breve descripcion sobre la categoria a crear.
        /// 
        /// Image: Boton para cargar una nueva imagen.
        /// </remarks>
        /// <param name="categorieCreationDTO">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>
        /// <response code="200">Ok. Objeto correctamente insertado en la BD.</response>   
        /// <response code="500">Internal Server Error. Ha ocurrido un error interno en el servidor y no se pudo llevar a cabo la peticion.</response>
        [Authorize(Roles = "1")]
        [HttpPost]
        [ProducesResponseType(typeof(Response<CategoriesGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CategoriesGetDTO>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostCategories([FromForm]CategorieCreationDTO categorieCreationDTO)
        {
            if (ModelState.IsValid)
            {
                Response<CategoriesGetDTO> result = new Response<CategoriesGetDTO>();
              
                result = await _categoriesBusiness.Insert(categorieCreationDTO);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500,result);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // DELETE: /Categories/5
        /// <summary>
        /// Elimina una categoria por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina de forma virtual la categoria indicada a travez de su Id.
        /// </remarks>
        /// <param name="id">Id del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Objeto borrado correctamente.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="403">Unauthorized. Usted no puede eliminar un objeto.</response>  
        /// <response code="500">Internal Server Error. Ha ocurrido un error interno en el servidor y no se pudo llevar a cabo la peticion.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var result = _categoriesBusiness.GetById(id);
            if (result != null)
            {
                var response = await _categoriesBusiness.Delete(id);

                if (!response.Succeeded)
                {
                    return StatusCode(500, response);
                }
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
