using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class UsersBusiness : IUsersBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private IEmailHelper _emailHelper;
        private readonly EntityMapper mapper = new EntityMapper();


        public UsersBusiness(IConfiguration configuration,IUnitOfWork unitOfWork, IEmailHelper emailHelper) 
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _emailHelper = emailHelper;
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
         var users = await _unitOfWork.UsersRepository.GetAll();
          List<UserDTO> userDTOs =  mapper.ToUsersListDTO(users);
            return userDTOs;
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> Insert(Users user)  
        {
            var pass = ApiHelper.Encrypt(user.Password);
            user.RolesId = 1;
            user.Password = pass;

            await _unitOfWork.UsersRepository.Insert(user);
            await _unitOfWork.SaveChangesAsync();
            await _emailHelper.SendEmail(user.Email, $"Bienvenido a nuestra Ong {user.FirstName}", "Ya podes Utilizar la Api");
            return user;
        }

        public async Task<Response<UserUpdateDTO>> Update(int id, UserUpdateDTO user)
        {
            Response<UserUpdateDTO> response = new Response<UserUpdateDTO>();
            var test = await _unitOfWork.UsersRepository.GetById(id);

            if (test == null)
            {
                
                    response.Succeeded = false;
                    response.Message = $"There is no User with ID: {id}";
                    return response;
                
            }
           
            
            Users UserToUpdate = mapper.UsersDTOToUserUpdate(id, user);
            var pass = ApiHelper.Encrypt(UserToUpdate.Password);
            UserToUpdate.Password = pass;
            UserToUpdate.Email = test.Email;
            

            await _unitOfWork.UsersRepository.Update(UserToUpdate);
            await _unitOfWork.SaveChangesAsync();

            UserDTO UserUpdated = mapper.ToUsersDTO(UserToUpdate);

            user.Password = "";


            response.Data = user;
            response.Succeeded = true;

            return response;

        }
    }
}
