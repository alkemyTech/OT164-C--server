using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICategoriesBusiness
    {
        Task GetAll();
        Task GetById(int id);
        Task Insert();
        Task Delete(int id);
        Task Update();
    }
}
