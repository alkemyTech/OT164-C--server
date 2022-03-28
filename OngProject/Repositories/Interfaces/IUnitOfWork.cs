﻿using Microsoft.EntityFrameworkCore;
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

        IRepository<News> NewsRepository { get; }
        IRepository<Categories> CategoriesRepository { get; }
<<<<<<< Updated upstream
=======

        IRepository<Organizations> OrganizationsRepository { get; }
        IRepository<Slides> SlidesRepository { get; }
        IRepository<Users> UsersRepository { get; }
        IUserAuthRepository UserAuthRepository { get; }
>>>>>>> Stashed changes
    }
}
