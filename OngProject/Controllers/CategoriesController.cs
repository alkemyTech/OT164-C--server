using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
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
            throw new NotImplementedException();
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
    }
}
