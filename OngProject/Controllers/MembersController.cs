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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly IMemberBusiness _memberBusiness;
        private readonly string contenedor = "Members";

        public MembersController(IMemberBusiness memberBusiness, IFileManager fileManager)
        {
            _memberBusiness = memberBusiness;
            _fileManager = fileManager;
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
        public async Task<ActionResult<Response<string>>> Post([FromForm] MembersCreateDTO member)
        {
            string imagePath = "";
            try
            {
                var extension = Path.GetExtension(member.Image.FileName);
                imagePath = await _fileManager.UploadFileAsync(member.Image, extension, contenedor, member.Image.ContentType);
            }
            catch (Exception e)
            {
                return new Response<string>(null)
                {
                    Errors = new string[] { e.Message },
                    Succeeded = false,
                };
            }
            await _memberBusiness.Insert(member, imagePath);
            return new Response<string>("") { Message = "Created succesfully" };
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Response<MembersDTO> response = await _memberBusiness.Delete(id);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Message);
        }
    }

}
