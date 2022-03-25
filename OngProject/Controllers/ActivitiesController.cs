using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController: ControllerBase
    {
        private readonly IActivitiesBusiness activities;

        public ActivitiesController(IActivitiesBusiness activities)
        {
            this.activities = activities;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Activities activities)
        {
            throw new NotImplementedException();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
