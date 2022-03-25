using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;

        public AuthController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreationDTO user)
        {
            try
            {
                var userEntity = await _usersBusiness.Insert(user);

                if (userEntity != null)
                {
                    return new JsonResult(userEntity);
                }

                return new JsonResult(Conflict());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

    }
}
