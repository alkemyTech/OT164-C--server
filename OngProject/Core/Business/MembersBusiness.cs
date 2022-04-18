using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
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
    public class MembersBusiness : IMemberBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();
        private readonly HttpContext context;

        public MembersBusiness(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            context = httpContextAccessor.HttpContext;
        }

        public async Task<PagedResponse<List<MembersGetDTO>>> GetAll(Filtros filtros)
        {
            var collection = await _unitOfWork.MembersRepository.GetAll() as IQueryable<Members>;

            var pagedData = await _unitOfWork.MembersRepository.GetAllPaged(collection, filtros.Pagina, filtros.CantidadRegistrosPorPagina, filtros, context);
            PagedResponse<List<MembersGetDTO>> result = new PagedResponse<List<MembersGetDTO>>();
            result = mapper.PagedResponseMembersDTO(pagedData);

            return result;

        }



        public async Task<Response<MembersGetDTO>> GetById(int id)
        {
            Response<MembersGetDTO> response = new Response<MembersGetDTO>();

            var query = await _unitOfWork.MembersRepository.GetById(id);
            if (query == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no members with ID: {id}";
                return response;
            }
            MembersGetDTO data = mapper.ToMembersDTO(query);
            response.Succeeded = true;
            response.Data = data;
            return response;
        }

        public async Task Insert(MembersCreateDTO members,string imagePath)
        {
            var m = mapper.MemberDTOToMembers(members,imagePath);
            await _unitOfWork.MembersRepository.Insert(m);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Response<MembersDTO>> Delete(int id)
        {
            Response<MembersDTO> response = new Response<MembersDTO>();
            var memberId = await _unitOfWork.MembersRepository.GetById(id);

            if (memberId == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no Member with ID: {id}";
                return response;
            }

            await _unitOfWork.MembersRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            response.Succeeded = true;
            response.Message = "Member was deleted successfully.";
            return response;
        }
        
        public async Task<bool> Update(RequestUpdateMembersDto updateMembersDto, int id)
        {
            Members members = await _unitOfWork.MembersRepository.GetById(id);
            if (members != null)
            {
                Members membersData = mapper.ToMembersFromDto(updateMembersDto, id);
                await _unitOfWork.MembersRepository.Update(membersData);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
              
            }
        }

      
    }
}
