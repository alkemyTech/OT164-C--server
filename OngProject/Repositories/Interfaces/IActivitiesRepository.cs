using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IActivitiesRepository : IRepository<Activities>
    {
        Task<bool> GetByName(string name);
        Task<bool> GetByContent(string content);
    }
}
