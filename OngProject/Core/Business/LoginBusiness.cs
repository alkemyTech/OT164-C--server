using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHelper _jwtHelper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginBusiness(IUnitOfWork unitOfWork, IJwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<ResponseLoginDto>> Login(RequestLoginModelDto login)
        {
            var user = await GetLoginByCredentials(login);
            Response<ResponseLoginDto> response = new Response<ResponseLoginDto>();
            if (user != null)
            {
                if (ApiHelper.Encrypt(login.Password) == user.Password)
                {
                    ResponseUserDto userResponse = new ResponseUserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Photo = user.Photo,
                        Email = user.Email,
                        RolesId = user.RolesId
                    };

                    string token = _jwtHelper.GetToken(user);
                    
                    ResponseLoginDto responseLogin = new ResponseLoginDto() { Token = token, User = userResponse };
                    response.Data = responseLogin;
                    response.Succeeded = true;
                    return response;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Ok: Error. La contraseña no coincide";
                    // Contraseña no coincide
                    return response;
                }
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Ok: Error. El usurio no existe";
                // Contraseña no coincide
                return response;
                //usuario no exixste
                
            }
        }

        private async Task<Users> GetLoginByCredentials(RequestLoginModelDto userLogin)
        {
            return await _unitOfWork.UserAuthRepository.GetLoginByCredentials(userLogin);
        }

        private async Task<Users> GetUserAuthenticated(string email)
        {
            return await _unitOfWork.UserAuthRepository.GetUserAuthenticated(email);
        }

        public ResponseUserDto GetUserLogged()
        {
            ResponseUserDto userDto = new ResponseUserDto();
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                var user = GetUserAuthenticated(userEmail).Result;

                userDto = new ResponseUserDto
                {

                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Photo = user.Photo,
                     RolesId = user.RolesId
                };

            }
            catch (Exception)
            {

              
            }

            return userDto;
        }
    }
}
