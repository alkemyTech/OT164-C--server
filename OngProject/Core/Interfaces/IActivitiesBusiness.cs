using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivitiesBusiness
    {
        public Task<List<Activities>> GetAll();

        public Task<Activities> GetById();
        public Task Insert(Activities activities);
        public Task Update(Activities activities);
        public Task Delete(Activities activities);

    }
}
