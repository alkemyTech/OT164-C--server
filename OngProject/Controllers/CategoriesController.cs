using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBusiness categoriesBusiness;

        public CategoriesController(ICategoriesBusiness categoriesBusiness)
        {
            this.categoriesBusiness = categoriesBusiness;
        }


        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var data = categoriesBusiness.GetAll();
            List<CategoriesGetDTO> result = new List<CategoriesGetDTO>();
            result = ConvertToListDTO(data.Result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoriesById(int id)
        {
            throw new NotImplementedException();
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

        public List<CategoriesGetDTO> ConvertToListDTO(IEnumerable<Categories> data)
        {
            List<CategoriesGetDTO> dtoList = new List<CategoriesGetDTO>();
            foreach (Categories e in data)
            {
                dtoList.Add(ConvertToDto(e));
            }
            return dtoList;
        }
        public CategoriesGetDTO ConvertToDto(Categories data)
        {
            var dataDto = new CategoriesGetDTO()
            {
                Name = data.Name
            };

            return dataDto;
        }
    }
}
