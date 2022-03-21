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

        Task Insert(Users rol);

        Task Delete(int id);

        Task Update(Users rol);
    }
}
