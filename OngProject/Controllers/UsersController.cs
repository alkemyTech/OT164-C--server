using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;

        public UsersController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ResponseCategoriesDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseCategoriesDetailDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetAllAsync() => Ok(await _usersBusiness.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

    

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }


        [HttpPut]
        public async Task<ActionResult> Update(Users users)
        {
            throw new NotImplementedException();
        }
    }
}
