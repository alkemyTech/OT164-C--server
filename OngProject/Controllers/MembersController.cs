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
    [Route("members")]
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(RequestUpdateMembersDto updateMembersDto, int id)
        {
            try
            {
                if (await _memberBusiness.Update(updateMembersDto, id))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
          
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
