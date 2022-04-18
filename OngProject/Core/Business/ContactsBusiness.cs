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
    public class ContactsBusiness : IContactsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailHelper _emailHelper;
        private readonly EntityMapper mapper = new EntityMapper();

        public ContactsBusiness(IUnitOfWork unitOfWork, IEmailHelper emailHelper)
        {
            _unitOfWork = unitOfWork;
            _emailHelper = emailHelper;
        }

        public async Task<Response<List<ContactsGetDTO>>> GetAll()
        {
            Response<List<ContactsGetDTO>> response = new Response<List<ContactsGetDTO>>();
            try
            {
                var result = await _unitOfWork.ContactsRepository.GetAll();
                response.Succeeded = true;
                response.Data = mapper.ToContactsListDTO(result);
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Succeeded = false;
                return response;
            }
        }

        public async Task<Response<ContactsGetDTO>> GetById(int id)
        {
            Response<ContactsGetDTO> response = new Response<ContactsGetDTO>();
            try
            {
                var contact = await _unitOfWork.ContactsRepository.GetById(id);

                if (contact == null)
                {
                    response.Succeeded = false;
                    response.Message = $"No existe el Contacto con el ID: {id}";
                    return response;
                }

                ContactsGetDTO data = mapper.ToContactsDTO(contact);
                response.Data = data;
                response.Succeeded = true;
                return response;
            }
            catch (Exception e)
            {
                response.Succeeded = false;
                response.Message = e.Message;
                return response;
            }
        }

        public async Task<Response<ContactsGetDTO>> Insert(ContactsGetDTO contactsGetDTO)
        {
            Response<ContactsGetDTO> response = new Response<ContactsGetDTO>();
            Contacts contact = mapper.ToContactsFromDTO(contactsGetDTO);

            try
            {
                await _unitOfWork.ContactsRepository.Insert(contact);
                await _emailHelper.SendEmail(contact.email, $"Gracias {contact.name} por ponerte en contacto con nosotros!", "Te responderemos a la brevedad.");
                await _unitOfWork.SaveChangesAsync();

                response.Data = contactsGetDTO;
                response.Message = "Se ha registrado los datos correctamente.";
                response.Succeeded = true;
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Succeeded = false;
                return response;
            }
        }

        public async Task<Response<ContactsGetDTO>> Update(ContactsGetDTO contactsGetDTO, int id)
        {
            Response<ContactsGetDTO> response = new Response<ContactsGetDTO>();

            try
            {
                var result = await _unitOfWork.ContactsRepository.GetById(id);
                if (result == null)
                {
                    response.Errors[1] = "404";
                    response.Succeeded = false;
                    response.Message = $"No existe el Contacto con el ID: {id}";
                    return response;
                }

                Contacts contact = mapper.ContactsUpdateDtoToContact(contactsGetDTO,id);
                await _unitOfWork.ContactsRepository.Update(contact);
                await _unitOfWork.SaveChangesAsync();
                response.Data = contactsGetDTO;
                response.Message = "Actualizacion realizada correctamente.";
                response.Succeeded = true;
                return response;
            }
            catch (Exception e)
            {
                response.Errors[1] = e.HResult.ToString();
                response.Message = e.Message;
                response.Succeeded = false;
                return response;
            }
        }

        public async Task<Response<ContactsGetDTO>> Delete(int id)
        {
            Response<ContactsGetDTO> response = new Response<ContactsGetDTO>();
            try
            {
                var result = await _unitOfWork.ContactsRepository.GetById(id);
                if (result == null)
                {
                    response.Succeeded = false;
                    response.Message = $"No existe el Contacto con el ID: {id}";
                    return response;
                }

                ContactsGetDTO data = mapper.ToContactsDTO(result);
                await _unitOfWork.ContactsRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();

                response.Succeeded = true;
                response.Message = "El Contacto fue borrado exitosamente.";
                response.Data = data;
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Succeeded = false;
                return response;
            }
        }
    }
}
