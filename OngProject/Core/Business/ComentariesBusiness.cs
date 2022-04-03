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

        public async Task<List<ComentariesFromNewsDTO>> GetAll()
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

        public async Task Insert(RequestComentariesDto comentariesDto)
        {
            News news = await _unitOfWork.NewsRepository.GetById(comentariesDto.NewsId);
            if (news != null)
            {
                Users users = await _unitOfWork.UsersRepository.GetById(comentariesDto.UserId);
                if (users != null)
                {
                    Comentaries comentaries = mapper.ToComentariesFromDto(comentariesDto);
                    await _unitOfWork.ComentariesRepository.Insert(comentaries);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    NullReferenceException userException = new NullReferenceException("Id de Usuario no valido");
                    throw userException;
                }
            }
            else
            {
                NullReferenceException newsException = new NullReferenceException("Id de Noticia no valido");
                throw newsException;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(RequestUpdateComentariesDto comentariesDto, int id)
        {
            Comentaries comentaries = await _unitOfWork.ComentariesRepository.GetById(id);
            if (comentaries != null)
            {
                Users users = await _unitOfWork.UsersRepository.GetById(comentariesDto.UserId);
                if (users != null && comentaries.UserId == users.Id)
                {
                    Comentaries comentariesUpdate = mapper.ToComentariesUpdateFromDto(comentariesDto, id);
                    comentariesUpdate.NewsId = comentaries.NewsId;
                    await _unitOfWork.ComentariesRepository.Update(comentariesUpdate);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else
                {
                    UserException userException = new UserException();
                    throw userException;
                }
            }
            else
            {
                return false;
            }
        }
    }


    [Serializable]
    public class UserException : Exception
    {
        public UserException() { }
        public UserException(string message) : base(message) { }
        public UserException(string message, Exception inner) : base(message, inner) { }
        protected UserException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
