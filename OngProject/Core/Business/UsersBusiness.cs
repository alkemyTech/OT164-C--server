using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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

        public async Task<Users> Insert(UserCreationDTO userDTO)  
        {
            var pass = ApiHelper.Encrypt(userDTO.Password);
            var user = new Users
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = pass,
                RolesId = 1
            };

            await _unitOfWork.UsersRepository.Insert(user);
            await _unitOfWork.SaveChangesAsync();
            await _emailHelper.SendEmail(userDTO.Email, $"Bienvenido a nuestra Ong {userDTO.FirstName}", "Ya podes Utilizar la Api");
            return user;
        }

        public Task Update(Users rol)
        {
            throw new NotImplementedException();
        }

        

        

        
    }
}
