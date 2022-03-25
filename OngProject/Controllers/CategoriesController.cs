using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Mapper;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBusiness _categoriesBusiness;

        public CategoriesController(ICategoriesBusiness categoriesBusiness)
        {
            _categoriesBusiness = categoriesBusiness;
        }


        [HttpGet]
        public async Task<ActionResult<List<CategoriesGetDTO>>> GetCategories()
        {
            var data = await _categoriesBusiness.GetAll();
            if(data == null)
            {
                return NoContent();
            }

            return Ok(data);


        }

        [HttpGet("{id}")]
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


        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategories(int id)
        {
            throw new NotImplementedException();

        }

        [HttpPost]
        public async Task<ActionResult> PostCategories()
        {
            throw new NotImplementedException();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategories(int id)
        {
            throw new NotImplementedException();
        }
    }
}
