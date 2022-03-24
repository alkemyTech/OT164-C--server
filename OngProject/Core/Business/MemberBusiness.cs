using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class MemberBusiness : IMemberBusiness
    {
        //private UnitOfWork unitOfWork = new UnitOfWork(context);
        private Repository<Members> repository;


        public MemberBusiness()
        {
          //  repository = new Repository<Members>(unitOfWork);
            
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert()
        {
            throw new NotImplementedException();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }


        //public async Task<IEnumerable<MembersDTO>> GetAllMembers()
        //{
        //    var data = await repository.GetAll();

        //    var result = ConvertToListDTO(data);

        //    return result;
        //}

        //IEnumerable<MembersDTO> ConvertToListDTO(IEnumerable<Members> data)
        //{
        //    List<MembersDTO> dataDTO = new List<MembersDTO>();
        //    if(data == null) { return null; }

        //    foreach (Members item in data)
        //    {
        //        dataDTO.Add(ConvertToDto(item));
        //    }

        //    return dataDTO;
        //}

        //MembersDTO ConvertToDto(Members data)
        //{
        //    var dataDto = new MembersDTO()
        //    {

        //        name = data.name,
        //        image = data.image,
        //        description = data.description
        //    };


        //    return dataDto;
        //}


    }
}
