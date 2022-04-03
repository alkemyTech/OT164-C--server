using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
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

        [Authorize(Roles = "1, 2")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(RequestUpdateComentariesDto updateComentaries, int id)
        {
            try
            {
                if (await _comentariesBusiness.Update(updateComentaries, id))
                {
                    Response<RequestUpdateComentariesDto> response = new Response<RequestUpdateComentariesDto>
                    {
                        Succeeded = true,
                        Message = "Se actualizo el comentario con id " + id,
                        Data = updateComentaries
                    };
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (UserException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

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
