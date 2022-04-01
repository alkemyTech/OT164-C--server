using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public void Dispose();
        public void SaveChanges();
        public Task SaveChangesAsync();

        IRepository<Activities> ActivitiesRepository { get; }
        IRepository<News> NewsRepository { get; }
        IRepository<Categories> CategoriesRepository { get; }

        IRepository<Organizations> OrganizationsRepository { get; }

        IRepository<Users> UsersRepository { get; }
        IUserAuthRepository UserAuthRepository { get; }

        IActivitiesRepository CustomActivitiesRepository { get; }
        IRepository<Slides> SlidesRepository { get; }

        IRepository<Members> MembersRepository { get; }

        IRepository<Comentaries> ComentariesRepository { get; }
        IRepository<Contacts> ContactsRepository { get; }
    }
}
