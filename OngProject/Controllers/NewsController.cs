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
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsBusiness;
        private readonly ICommentsRepository _commentsRepository;

        public NewsController(INewsBusiness newsBusiness, ICommentsRepository commentsRepository)
        {
            _newsBusiness = newsBusiness;
            _commentsRepository = commentsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<NewsDTO>>>> GetAllNews([FromQuery] Filtros filtros)
        {
            var data = await _newsBusiness.GetAllNews(filtros);
            if(data != null)
            {
                return Ok(data);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NewsGetByIdDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NewsGetByIdDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetNewsById(int id)
        {

            Response<NewsGetByIdDTO> response =  _newsBusiness.GetNewsById(id);

            
            if (!response.Succeeded)
            {
                
                
                return NotFound(response.Message);

            }


                return Ok(response.Data);
            
        }

        [HttpGet("{id:int}/comments")]
        public async Task<ActionResult<List<ComentariesFromNewsDTO>>> GetCommentsFromNew(int id)
        {
            var comentaries = await _commentsRepository.GetComementsFromNew(id);

            if (comentaries == null)
                return NoContent();

            return Ok(comentaries);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PostNews(NewsDTO news)
        {
            Response<NewsDTO> response = await _newsBusiness.CreateNews(news);

            if (!response.Succeeded) {

                return NotFound(response);
            
            }

            return Ok(response);

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles ="1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewsDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutNews(NewsDTO newsDTO, int id)
        {
            NewsDTO newsUpdate = await _newsBusiness.UpdateNews(newsDTO, id);
            if (newsUpdate != null)
            {
                return Ok(new Response<NewsDTO>
                {
                    Message = "Se actualizo correctamente la entidad",
                    Data = newsDTO,
                    Succeeded = true      
                });
            }
            else
            {
                return NotFound(new Response<NewsDTO>
                {
                    Message = "No se modifico la entidad",
                    Data = newsDTO,
                    Succeeded = false,
                    Errors = new string[] { }
                });
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DeleteNews(int id)
        {
            var response = await _newsBusiness.DeleteNews(id);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
