using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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
        private readonly EntityMapper mapper = new EntityMapper();


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
            string otherEntiy = "Slides";
            Task<IEnumerable<Organizations>> OrganizationsData = _unitOfWork.OrganizationsRepository.GetAllIncludeAsync(otherEntiy);


            if (OrganizationsData.Result == null)
            {
                return null;
            }

            List<OrganizationsPublicDTO> result = mapper.ToOrgPublicDTO(OrganizationsData);

           
           result = result.OrderBy(x => x.Slides.Min(s => s.orden.Length)).ToList();
            

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
