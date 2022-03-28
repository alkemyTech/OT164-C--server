using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult> GetAllNews()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetNewsById(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id:int}/comments")]
        public async Task<ActionResult<List<ComentariesFromNewsDTO>>> GetCommentsFromNew(int id)
        {
            var comentaries =  await _commentsRepository.GetComementsFromNew(id);

            if (comentaries == null)
                return NoContent();

            return Ok(comentaries);
        }

        [HttpPost]
        public async Task<ActionResult> PostNews()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutNews(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
