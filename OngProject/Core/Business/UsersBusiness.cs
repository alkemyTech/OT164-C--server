﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
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
        private readonly IRepository<Users> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private string _entity = "Roles";

        public UsersBusiness(IConfiguration configuration, IRepository<Users> repository,IUnitOfWork unitOfWork) 
        {
            _configuration = configuration;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _unitOfWork.UsersRepository.GetAllIncludeAsync(_entity);
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

            await _repository.Insert(user);

            return user;
        }

        public Task Update(Users rol)
        {
            throw new NotImplementedException();
        }

        

        

        
    }
}
