using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IFileManager _fileManager;
        private EntityMapper mapper = new EntityMapper();
        private readonly string contenedor = "Users";
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IJwtHelper _jwtHelper;

        //public AuthController(IUsersBusiness usersBusiness, ILoginBusiness loginBusiness, IFileManager fileManager)
        //{
        //    _usersBusiness = usersBusiness;
        //    _loginBusiness = loginBusiness;
        //    this._fileManager = fileManager;
        //}
        public AuthController(IUsersBusiness usersBusiness, ILoginBusiness loginBusiness, IFileManager filemanager, IUserAuthRepository userAuthRepository, IJwtHelper jwtHelper)
        {
            _usersBusiness = usersBusiness;
            _loginBusiness = loginBusiness;
            _fileManager = filemanager;
            _userAuthRepository = userAuthRepository;
            _jwtHelper = jwtHelper;
        }

    

        [HttpPost("register")]
        public async Task<ActionResult<Response<Users>>> Register([FromForm] UserCreationDTO userCreacionDto)
        {
            Response<ResponseRegisterUserDto> result = new Response<ResponseRegisterUserDto>();
            var user = mapper.UserDtoTOUsers(userCreacionDto);
            Tools tools = new Tools(userCreacionDto.Photo, contenedor, _fileManager);
            Response<string> imageResult = await tools.EvaluateImage();

            ResponseRegisterUserDto response = new ResponseRegisterUserDto();

            if (imageResult.Succeeded)
            {
                user.Photo = imageResult.Data;
            }

            try
            {
                var exist = await _userAuthRepository.GetUserAuthenticated(user.Email);
                if (exist != null)
                {
                    result.Succeeded = false;
                    result.Message = "This email is already taken. Try using another email";
                    result.Data = response;
                    return BadRequest(result);
                }
               

                var userEntity = await _usersBusiness.Insert(user);
                
                response.Token = _jwtHelper.GetToken(user);
                response.User = user;
                if (userEntity != null)
                {
                    result.Succeeded = true;
                  
                    result.Data = response;
                    return Ok(result);
                }
                result.Succeeded = false;
                result.Message = "Error: something wrong happend.";
                result.Data = response;
                return BadRequest(result);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Succeeded = false;
                result.Data = response;
                return BadRequest(result);
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
