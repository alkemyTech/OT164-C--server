using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
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

        public ResponseOrganizationsDetailDto GetById(int id)
        {
            var query = _unitOfWork.OrganizationsRepository.GetById(id);
            if (query.Result == null)
            {
                return null;
            }
            return new ResponseOrganizationsDetailDto
            {
                OrganizationId = query.Result.Id,
                Name = query.Result.Name,
                Image = query.Result.Image,
                Email = query.Result.Email
            };

        }

        public Task Insert(Organizations entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<OrganizationsUpdateDTO>> Update(OrganizationsUpdateDTO organizationsUpdatePublicDTO, int id)
        {
            Response<OrganizationsUpdateDTO> response = new Response<OrganizationsUpdateDTO>();
            OrganizationsUpdateDTO result = new OrganizationsUpdateDTO();
            var entity = mapper.OrganizationUpdateDTOToOrganizations(organizationsUpdatePublicDTO, id);

            try
            {
                await _unitOfWork.OrganizationsRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                response.Succeeded = true;
            }
            catch(Exception e)
            {
                response.Succeeded = false;
                response.Message = e.Message;
                return response;
            }

            OrganizationsUpdateDTO organizationDTO = mapper.OrganizationsToOrganizationUpdateDTO(entity);
            response.Data = organizationDTO;
            return response;
        }
    }
}
