using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;
        private readonly ILoginBusiness _loginBusiness;

        public AuthController(IUsersBusiness usersBusiness, ILoginBusiness loginBusiness)
        {
            _usersBusiness = usersBusiness;
            _loginBusiness = loginBusiness;
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

        [HttpPost]
        [Route("login")]
        public IActionResult PostLogin([FromBody] RequestLoginModelDto loginModelDto)
        {
            try
            {
                return new JsonResult(_loginBusiness.Login(loginModelDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("me")]
        [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status200OK)]
        public IActionResult GetUserLogged()
        {
            try
            {
                return new JsonResult(_loginBusiness.GetUserLogged()) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }

    }
}
