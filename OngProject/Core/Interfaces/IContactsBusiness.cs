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
        Task<List<ContactsGetDTO>> GetAll();
        Task GetById(int id);
        Task Insert(ContactsGetDTO contacts);
        Task Delete(int id);
        Task Update();
    }
}
