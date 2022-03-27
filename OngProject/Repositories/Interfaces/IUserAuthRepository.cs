using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUserAuthRepository : IRepository<Users>
    {
        Task<Users> GetLoginByCredentials(RequestLoginModelDto login);
        Task<Users> GetUserAuthenticated(string email);
    }
}
