using Microsoft.AspNetCore.Authorization;
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
    [Route("Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsBusiness _contactsBusiness;

        public ContactsController(IContactsBusiness contactsBusiness)
        {
            _contactsBusiness = contactsBusiness;
        }

        [HttpGet]
        [Authorize (Roles = "1")]
        public async Task<ActionResult<List<Contacts>>> GetAll()
        {
            var data = await _contactsBusiness.GetAll();
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
        public async Task<ActionResult> Post([FromForm]ContactsGetDTO contact)
        {
            try
            {
                if (contact.Name == null)
                    return BadRequest("El campo Nombre no puede estar vacio");

                if (contact.Email == null)
                    return BadRequest("El campo Email no puede estar vacio");

                await _contactsBusiness.Insert(contact);

                return NoContent();
            }catch(Exception e)
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
