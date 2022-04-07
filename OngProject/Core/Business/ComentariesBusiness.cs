using Microsoft.AspNetCore.Http;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ComentariesBusiness : IComentariesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ComentariesBusiness(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<Response<ComentariesByIdDTO>> Delete(int id)
        {
            

            Response<ComentariesByIdDTO> response = new Response<ComentariesByIdDTO>();


            var comment = await _unitOfWork.ComentariesRepository.GetById(id);

            

            if (comment == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no comentaries with ID: {id}";
                response.Errors = new string[1];
                response.Errors[0] = "404"; 
                return response;
            }


            if (checkUser(comment.UserId))
            {
                await _unitOfWork.ComentariesRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();

                response.Succeeded = true;
                response.Message = "Comment deleted successfully.";
                return response;



           }

           else
            {

                response.Succeeded = false;
                response.Message = "This comment belongs to another user, you cannot delete it";
                response.Errors = new string[1];
                response.Errors[0] = "403";
                return response; 
            }





        }

        private async Task<Users> GetUserAuthenticated(string email)
        {
            return await _unitOfWork.UserAuthRepository.GetUserAuthenticated(email);
        }

        public bool checkUser(int userId) 
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = GetUserAuthenticated(userEmail).Result;

            if(user.Id == userId)
            {
                return true;
            }

            else
            {
                return false;
            }
            


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

        public async Task<Response<ComentariesByIdDTO>> GetById(int id)
        {
            

            Response<ComentariesByIdDTO> response = new Response<ComentariesByIdDTO>();

            var query = await _unitOfWork.ComentariesRepository.GetById(id);


            if (query == null)
            {
                response.Succeeded = false;
                response.Message = $"There is no comentaries with ID: {id}";

                return response;
            }

            ComentariesByIdDTO data = mapper.ComentariesByIdToDTO(query);

            response.Data = data;
            response.Succeeded = true;

            return response;




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
