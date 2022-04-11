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
    /// <summary>
    /// Web API para login y registro de usuarios de la ONG.
    /// </summary>

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


        // POST: /auth/register
        /// <summary>
        /// Registra a un nuevo usuario en la BD.
        /// </summary>
        /// <remarks>
        /// El usuario completa un formulario con datos que pasarán a un archivo Json, que serán convertido en un objeto antes de insertarse en la BD 
        /// 
        /// Sample Request:
        /// 
        /// FirstName: Primer nombre del usuario (string)
        /// 
        /// LastName: Apellido del usuario   (string)
        /// 
        /// Email: Email del usuario, una vez que este registrado será necesario para loguearse (string, formato email)
        /// 
        /// Password: Contraseña del usuario, se guardará de forma segura al encriptarse   (string)
        /// 
        /// Image: Boton para seleccionar imagen, puede registrarse sin utilizar una imagen
        /// </remarks>
        /// <param name="userCreacionDto">Usuario a registrar en la BD.</param>
        /// <response code="200">Ok. Usuario registrado correctamente en la BD.</response>    
        /// <response code="400">El Email proporcionado ya se encuentra en uso o hubo un error en el registro</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(Response<Users>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Users>), StatusCodes.Status400BadRequest)]
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


        // POST: /auth/login
        /// <summary>
        /// Login de usuario
        /// </summary>
        /// <remarks>
        /// Usuario ingresa email y contraseña, se devuelve token necesario para autorizar el uso de la API
        /// 
        /// Sample Request:
        /// 
        /// Email: Email del usuario
        /// Password: Contraseña del usuario
        /// 
        /// </remarks>
        /// <param name="loginModelDto">Email y contraseña de usuario registrado</param>
        /// <response code="200">Ok. Usuario logueado correctamente.</response>    
        /// <response code="400">BadRequest. Error al intentar loguearse</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
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



        // GET: /auth/me
        /// <summary>
        /// Obtiene datos de usuario logueado actualmente
        /// </summary>
        /// <remarks>
        ///  Devuelve nombre y contraseña de usuario logueado
        /// </remarks>
        /// <response code="401">Unauthorized. Token no valido</response>              
        /// <response code="200">Ok. Datos de usuario devueltos correctamente.</response>        
        /// <response code="400">BadRequest. Error al intentar obtener datos</response>
        [HttpGet]
        [Route("me")]
        [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status401Unauthorized)]
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
