using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactsBusiness
    {
        Task<Response<List<ContactsGetDTO>>> GetAll();
        Task<Response<ContactsGetDTO>> GetById(int id);
        Task<Response<ContactsGetDTO>> Insert(ContactsGetDTO contacts);
        Task<Response<ContactsGetDTO>> Delete(int id);
        Task<Response<ContactsGetDTO>> Update(ContactsGetDTO contactsGetDTO, int id);
    }
}
