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

        public async Task<List<ContactsGetDTO>> GetAll()
        {
            var data = await _unitOfWork.ContactsRepository.GetAll();
            if (data != null)
            {
                return mapper.ToContactsListDTO(data);
            }

            return null;

        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(ContactsGetDTO contactDTO)
        {
            Contacts contact = mapper.ToContactsFromDTO(contactDTO);
            await _unitOfWork.ContactsRepository.Insert(contact);
            await _emailHelper.SendEmail(contact.email, $"Gracias {contact.name} por ponerte en contacto con nosotros!", "Te responderemos a la brevedad.");
            await _unitOfWork.SaveChangesAsync();
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
