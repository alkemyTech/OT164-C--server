using OngProject.Core.Models;
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

        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task GetById(int id);

        Task<Users> Insert(Users user);

        Task Delete(int id);

        Task<Response<UserUpdateDTO>> Update(int id, UserUpdateDTO user);
    }
}
