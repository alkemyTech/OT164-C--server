using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
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
        private readonly EntityMapper mapper = new EntityMapper();

        public OrganizationsController(IOrganizationsBusiness organizationsBusiness)
        {
            _organizationsBusiness = organizationsBusiness;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("public")]
        [ProducesResponseType(typeof(List<OrganizationsPublicDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<OrganizationsPublicDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPublic()
        {

            List<OrganizationsPublicDTO> result = new List<OrganizationsPublicDTO>();
            result = _organizationsBusiness.GetPublic();

            if (result != null)
            {
                return Ok(result);
            }

            else
            {
                return NotFound();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("public/{id:int}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> PutOrganization(int id, OrganizationsUpdateDTO organizationUpdateDTO)
        {
            Response<OrganizationsUpdateDTO> response = new Response<OrganizationsUpdateDTO>();

            var result = _organizationsBusiness.GetById(id);
            if (result != null)
            {
                response = await _organizationsBusiness.Update(organizationUpdateDTO, id);
                if (response.Succeeded)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
                
            }
            else
            {
                return NotFound(response);
            }
        }

    }
}
