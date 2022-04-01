using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
   
    public class ActivitiesController: ControllerBase
    {
        private readonly IActivitiesBusiness activities;
        private readonly IFileManager _fileManager;
        private EntityMapper mapper = new EntityMapper();
        private readonly string contenedor = "Activities";

        public ActivitiesController(IActivitiesBusiness activities, IFileManager filemanager)
        {
            this.activities = activities;
            this._fileManager = filemanager;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [Route("Activities")]
        [HttpPost]
        public async Task<ActionResult<Response<ActivitiesGetDto>>> Insert([FromForm]ActivitiesCreationDTO activitiesCreationDTO)
        {
            Response<ActivitiesGetDto> result = new Response<ActivitiesGetDto>();
            if (ModelState.IsValid)
            {
                var activity = mapper.ActivityCreationDTOToActivity(activitiesCreationDTO);
                if (activitiesCreationDTO.Image != null)
                {
                    try
                    {
                        var extension = Path.GetExtension(activitiesCreationDTO.Image.FileName);
                        activity.Image = await _fileManager.UploadFileAsync(activitiesCreationDTO.Image, extension, contenedor,
                        activitiesCreationDTO.Image.ContentType);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);

                    }

                }

                result = await activities.Insert(activity);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
          


           
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
