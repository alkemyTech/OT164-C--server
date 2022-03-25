using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
     public interface ISlidesBusiness
    {
        public Task<List<Slides>> GetAll();

        public Task<Slides> GetById();
        public Task Insert(Slides slides);
        public Task Update(Slides slides);
        public Task Delete(Slides slides);


    }
}
