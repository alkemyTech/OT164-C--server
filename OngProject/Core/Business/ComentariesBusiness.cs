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
    public class ComentariesBusiness : IComentariesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();

        public ComentariesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ComentariesGetDTO>> GetAll()
        {
            var data = await _unitOfWork.ComentariesRepository.GetAll();

            if (data != null)
            {
                data = data.OrderBy(x => x.DateModified);
                return mapper.ToComentariesListDTO(data);
            }

            return null;

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
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
