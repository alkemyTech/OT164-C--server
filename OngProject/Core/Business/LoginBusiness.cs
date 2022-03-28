using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
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

        public ResponseLoginDto Login(RequestLoginModelDto login)
        {
            var user = GetLoginByCredentials(login).Result;
            if (user != null)
            {
                if (ApiHelper.Encrypt(login.Password) == user.Password)
                {
                    ResponseUserDto userResponse = new ResponseUserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    string token = _jwtHelper.GetToken(user);

                    return new ResponseLoginDto { Token = token, User = userResponse };
                }
                else
                {
                    // Contraseña no coincide
                    throw new Exception("Ok: Error. La contraseña no coincide");
                }
            }
            else
            {
                //usuario no exixste
                throw new Exception("Ok: Error. El usurio no existe");
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
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = GetUserAuthenticated(userEmail).Result;

            return new ResponseUserDto
            {

                FirstName = user.FirstName,
                LastName = user.LastName
            };
            

        }
    }
}
