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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;

        public UsersController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
        }



      /*  [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        } */


        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAllAsync() => Ok(await _usersBusiness.GetAllAsync()); 



        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<Users>> Insert(UserCreationDTO user)
        {
            return await _usersBusiness.Insert(user);
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
