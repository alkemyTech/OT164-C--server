using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
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

    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _organizationsBusiness;

        public OrganizationsController(IOrganizationsBusiness organizationsBusiness)
        {
            _organizationsBusiness = organizationsBusiness;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            throw new NotImplementedException();

        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            throw new NotImplementedException();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
