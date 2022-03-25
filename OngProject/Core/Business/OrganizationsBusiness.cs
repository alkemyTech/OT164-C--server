using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class OrganizationsBusiness : IOrganizationsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

       

        public OrganizationsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Delete(Organizations entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Organizations>> GetAll()
        {
            throw new NotImplementedException();
        }


        public List<OrganizationsPublicDTO> GetPublic()
        {
            Task<IEnumerable<Organizations>> OrganizationsData = _unitOfWork.OrganizationsRepository.GetAll();

            if(OrganizationsData.Result == null)
            {
                return null;
            }
            List<OrganizationsPublicDTO> result = new List<OrganizationsPublicDTO>();
            
            foreach(Organizations org in OrganizationsData.Result)
            {
                OrganizationsPublicDTO organizationdto = new OrganizationsPublicDTO();
                organizationdto.Name = org.Name;
                organizationdto.Image = org.Image;
                organizationdto.Address = org.Address;
                organizationdto.Phone = org.Phone;
                result.Add(organizationdto);

            }

            return result;
                

            

           


        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Organizations entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Organizations entity)
        {
            throw new NotImplementedException();
        }

 
    }
}
