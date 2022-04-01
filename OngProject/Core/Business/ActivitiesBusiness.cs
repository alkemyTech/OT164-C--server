using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ActivitiesBusiness: IActivitiesBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();

        public ActivitiesBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task Delete(Activities activities)
        {
            throw new NotImplementedException();
        }

        public Task<List<Activities>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Activities> GetById()
        {
            throw new NotImplementedException();
        }
        public Task Update(Activities activities)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Response<ActivitiesGetDto>> Insert(Activities activities)
        {
            Response<ActivitiesGetDto> response = new Response<ActivitiesGetDto>();
            ActivitiesGetDto result = new ActivitiesGetDto();
            var existeContent  = await unitOfWork.CustomActivitiesRepository.GetByContent(activities.Content);
            if (existeContent)
            {
                response.Errors = new[] { "The content already exists" };
                response.Succeeded = false;
                response.Message = "The content already exists";
                return response;
            }

            var existeName = await unitOfWork.CustomActivitiesRepository.GetByName(activities.Name);
            if (existeName)
            {
                response.Errors = new[] { "The Name already exists" };
                response.Succeeded = false;
                response.Message = "The Name already exists";
                return response;
            }

            try
            {
                await unitOfWork.ActivitiesRepository.Insert(activities);
                await unitOfWork.SaveChangesAsync();
                result = mapper.ActivityToActivitiesGetDTO(activities);
                response.Succeeded = true;
                response.Data = result;
                
              
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Errors = new[] { e.InnerException.Message };
                response.Succeeded = false;
            }

            //await _unitOfWork.Ac.Insert(activities);
            //await _unitOfWork.SaveChangesAsync();
            //await _emailHelper.SendEmail(user.Email, $"Bienvenido a nuestra Ong {user.FirstName}", "Ya podes Utilizar la Api");
            //return user;
            return response;
        }
    }
}
