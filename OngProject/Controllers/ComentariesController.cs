using Microsoft.AspNetCore.Http;
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
    [Route("Comments")]
    [ApiController]
    public class ComentariesController : ControllerBase
    {
        private readonly IComentariesBusiness _comentariesBusiness;

        public ComentariesController(IComentariesBusiness comentariesBusiness)
        {
            _comentariesBusiness = comentariesBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comentaries>>> GetAll()
        {
            var data = await _comentariesBusiness.GetAll();
            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpGet("Id:int")]
        public async Task<ActionResult<Comentaries>> GetById(int Id)
        {
            return NoContent();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id)
        {
            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult> Post(RequestComentariesDto comentariesDto)
        {
            try
            {
                await _comentariesBusiness.Insert(comentariesDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }

        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult> Delete(int Id)
        {
            return NoContent();

        }


    }
}
