using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivitiesBusiness
    {
        public Task<List<ActivitiesDTO>> GetAll();

        public Task<ActivitiesDTO> GetById(int id);
        public Task<Response<ActivitiesGetDto>> Insert(Activities activities);
        Task<ActivitiesDTO> Update(ActivitiesDTO newsDTO, int id);
        public Task<Response<ActivitiesDTO>> Delete(int id);

    }
}
