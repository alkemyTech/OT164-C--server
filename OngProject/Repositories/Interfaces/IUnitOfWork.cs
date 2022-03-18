using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork<out TContext>
        where TContext : DbContext, new()

    {

        TContext Context { get; }
        public void Dispose();
        public void SaveChanges();
        public Task SaveChangesAsync();

        IRepository<News> NewsRepository { get; }
    }
}
