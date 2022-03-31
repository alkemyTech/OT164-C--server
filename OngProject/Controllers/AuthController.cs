using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
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

        //public AuthController(IUsersBusiness usersBusiness, ILoginBusiness loginBusiness, IFileManager fileManager)
        //{
        //    _usersBusiness = usersBusiness;
        //    _loginBusiness = loginBusiness;
        //    this._fileManager = fileManager;
        //}
        public AuthController(IUsersBusiness usersBusiness, ILoginBusiness loginBusiness, IFileManager filemanager, IUserAuthRepository userAuthRepository)
        {
            _usersBusiness = usersBusiness;
            _loginBusiness = loginBusiness;
            _fileManager = filemanager;
            _userAuthRepository = userAuthRepository;
        }

    

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] UserCreationDTO userCreacionDto)
        {

            var user = mapper.UserDtoTOUsers(userCreacionDto);
            if (userCreacionDto.Photo != null)
            {
                try
                {
                    var extension = Path.GetExtension(userCreacionDto.Photo.FileName);
                    user.Photo = await _fileManager.UploadFileAsync(userCreacionDto.Photo, extension, contenedor,
                    userCreacionDto.Photo.ContentType);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);

                }

            }

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
