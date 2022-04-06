using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
     public interface ISlidesBusiness
    {
        public Task<List<SlidesGetAllDTO>> GetAll();

        public Task<SlidesDTO> GetById(int id);
        public Task<Response<SlidesDTO>> Insert(SlidesDTO slidesDTO);
        public Task<bool> Update(SlidesDTO slides, int id);
        public Task Delete(Slides slides);


    }
}
