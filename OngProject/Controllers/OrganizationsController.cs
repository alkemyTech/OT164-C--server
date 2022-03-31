using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("organizations")]
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


        [HttpGet("public")]
        [ProducesResponseType(typeof(List<OrganizationsPublicDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<OrganizationsPublicDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPublic()
        {

            List<OrganizationsPublicDTO> result = new List<OrganizationsPublicDTO>();
            result = _organizationsBusiness.GetPublic();

            if (result != null)
            {
                return new JsonResult(result) { StatusCode = 200 };
            }

            else
            {
                return NotFound();
            }
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
