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

namespace OngProject.Controllers
{
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


        [HttpGet]
        public async Task<ActionResult<Response<List<CategoriesGetDTO>>>> GetCategories()
        {
            var data = await _categoriesBusiness.GetAll();
            Response<List<CategoriesGetDTO>> result = new Response<List<CategoriesGetDTO>>();
            if (data == null)
            {
                return NoContent();
            }
                     
            result.Succeeded = true;
            result.Data = data;
            return Ok(result);

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategories(int id)
        {
            throw new NotImplementedException();
        }
    }
}
