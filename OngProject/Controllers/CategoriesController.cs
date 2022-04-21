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
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>

        
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<PagedResponse<List<CategoriesGetDTO>>>> GetAll([FromQuery] Filtros filtros)
        {
            var data = await _categoriesBusiness.GetAll(filtros);
            return Ok(data);
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
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(typeof(ResponseCategoriesDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseCategoriesDetailDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCategoriesById(int id)
        {
            try
            {
                var result = _categoriesBusiness.GetById(id);
                if (result != null)
                {
                    return new JsonResult(result) { StatusCode = 200 };
                }
                else
                {
                    return NotFound();
                }
                
               
            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest(e.Message)) { StatusCode = 400 };
            }
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

        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCategories(int id, CategoriesUpdateDTO categoriesUpdateDTO)
        {
            var result = _categoriesBusiness.GetById(id);
            if (result != null)
            {
                var response = await _categoriesBusiness.Update(categoriesUpdateDTO, id);
               
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
        /// <response code="401">Unauthorized. Usted no tiene permisos para crear un nuevo objeto.</response>   
        /// <response code="403">Unauthorized. Usted no puede crear un nuevo objeto.</response>   
        /// <response code="200">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Response<CategoriesGetDTO>>> PostCategories(CategorieCreationDTO categorieCreationDTO)
        {
            if (ModelState.IsValid)
            {
                Response<CategoriesGetDTO> result = new Response<CategoriesGetDTO>();
                var categories = mapper.CategoriesCreationDTOToCategories(categorieCreationDTO);
                result = await _categoriesBusiness.Insert(categories);

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
        /// <response code="400">BadRequest. Ha ocurrido un error y no se pudo llevar a cabo la peticion.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<Response<CategoriesGetDTO>> DeleteCategories(int id)
        {
            var category = await _categoriesBusiness.Delete(id);
            Response<CategoriesGetDTO> response = new();

            if (!category)
            {
              response.Succeeded = false;
              response.Message = $"a problem occurred while trying to delete the category. Check if the category with the ID {id} exists.";
              return response;
            }

            response.Succeeded = true;
            response.Message = "Category deleted successfully!";
            return response;
        }
    }
}
