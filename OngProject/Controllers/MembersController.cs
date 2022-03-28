using Microsoft.AspNetCore.Authorization;
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
    [Produces("application/json")]
    [Route("Members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberBusiness _memberBusiness;

        public MembersController(IMemberBusiness memberBusiness)
        {
            _memberBusiness = memberBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<List<Members>>> GetAll()
        {
            var data = await _memberBusiness.GetAll();
            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpGet("Id:int")]
        public async Task<ActionResult<Members>> GetById(int Id)
        {
            return NoContent();
           
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id)
        {
            return NoContent();
          
        }

        [HttpPost]
        public async Task<ActionResult> Post(Members member)
        {
            return NoContent();
           
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult> Delete(int Id)
        {
            return NoContent();
          
        }


    }
}
