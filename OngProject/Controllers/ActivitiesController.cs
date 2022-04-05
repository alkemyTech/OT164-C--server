using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper;
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
      

        public ActivitiesController(IActivitiesBusiness activities, IFileManager fileManager)
        {
            this.activities = activities;
            this._fileManager = fileManager;
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
                
                Tools tools = new Tools(activitiesCreationDTO.Image, contenedor, _fileManager);
                Response<string> imageResult = await tools.EvaluateImage();
                if (imageResult.Succeeded)
                {
                    activity.Image = imageResult.Data;
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
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ActivitiesDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutNews(ActivitiesDTO activitiesDTO, int id)
        {
            ActivitiesDTO activitiesUdate = await activities.Update(activitiesDTO, id);
            if (activitiesUdate != null)
            {
                return Ok(new Response<ActivitiesDTO>
                {
                    Message = "Se actualizo correctamente la entidad",
                    Data = activitiesDTO,
                    Succeeded = true
                });
            }
            else
            {
                return NotFound(new Response<ActivitiesDTO>
                {
                    Message = "No se modifico la entidad",
                    Data = activitiesDTO,
                    Succeeded = false,
                    Errors = new string[] { }
                });
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
