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

        [Authorize(Roles = "1, 2")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comentaries>> GetById(int id)
        {
            


            Response<ComentariesByIdDTO> response = await _comentariesBusiness.GetById(id);

            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }

            return new JsonResult(response.Data) { StatusCode = 200 };


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

        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<ComentariesByIdDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Response<ComentariesByIdDTO> response = await _comentariesBusiness.Delete(id);

            if (!response.Succeeded)
            {
                if (response.Errors[0].Equals("404"))
                {

                    return NotFound(response.Message);
                }

                if (response.Errors[0].Equals("403"))
                {

                    return Forbid(response.Message);
                }


            }

            return new JsonResult(response) { StatusCode = 200 };
        }


    }
}
