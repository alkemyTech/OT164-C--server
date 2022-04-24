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
   
        private readonly IUnitOfWork _unitOfWork;
        private IEmailHelper _emailHelper;
        private readonly EntityMapper mapper = new EntityMapper();


        public UsersBusiness(IUnitOfWork unitOfWork, IEmailHelper emailHelper) 
        {
           
            _unitOfWork = unitOfWork;
            _emailHelper = emailHelper;
        }
        public async Task<Response<ResponseUserDto>> Delete(int id)
        {
            Response<ResponseUserDto> response = new Response<ResponseUserDto>();
            var userId = await _unitOfWork.UsersRepository.GetById(id);

            if (userId == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no User with ID: {id}";
                return response;
            }

            await _unitOfWork.UsersRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
           
            response.Succeeded = true;
            response.Message = "User deleted successfully.";
            return response;
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _unitOfWork.UsersRepository.GetAll();
            List<UserDTO> userDTOs = mapper.ToUsersListDTO(users);
            return userDTOs;
        }

        public async Task<Response<UserDTO>> GetById(int id)
        {
            Response<UserDTO> response = new Response<UserDTO>();

            var query = await _unitOfWork.UsersRepository.GetById(id);
            if (query == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no User with ID: {id}";
                return response;
            }
            UserDTO data = mapper.ToUsersDTO(query);
            response.Succeeded = true;
            response.Data = data;
            return response;
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
