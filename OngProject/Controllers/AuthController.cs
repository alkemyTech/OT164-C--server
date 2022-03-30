using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
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
        private readonly IUserAuthRepository _userAuthRepository;

        public AuthController(IUsersBusiness usersBusiness, ILoginBusiness loginBusiness, IUserAuthRepository userAuthRepository)
        {
            _usersBusiness = usersBusiness;
            _loginBusiness = loginBusiness;
            _userAuthRepository = userAuthRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreationDTO user)
        {
            try
            {
                var exist = await _userAuthRepository.GetUserAuthenticated(user.Email);
                if (exist != null)
                    return BadRequest("This email is already taken. Try using another email");

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
                var user = _loginBusiness.GetUserLogged();
                
                return new JsonResult(user) { StatusCode = 200 };
                
            }
            catch (NullReferenceException)
            {
                return Unauthorized("Token no valido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }

    }
}
