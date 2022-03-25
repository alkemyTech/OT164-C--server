using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
   public interface IUsersBusiness
    {
        Task GetAll();
        Task GetById(int id);

        Task<Users> Insert(UserCreationDTO user);

        Task Delete(int id);

        Task Update(Users rol);

        string GetToken(Users user);
    }
}
