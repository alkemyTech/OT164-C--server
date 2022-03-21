using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : DbContext, new()
    {

        private readonly TContext _context;

        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Users> _usersRepository;
        public UnitOfWork()
        {
            _context = new TContext();
        }


        public TContext Context
        {
            get { return _context; }
        }


        //public IRepository<TestimonialsModel> TestimonialsModelRepository => _testimonialsModelRepository ?? new Repository<TestimonialsModel>(_context);
        public IRepository<News> NewsRepository =>  _newsRepository ?? new Repository<News>((IUnitOfWork<DataAccess.ApplicationDbContext>)_context);
        public IRepository<Users> UsersRepository => _usersRepository ?? new Repository<Users>((IUnitOfWork<DataAccess.ApplicationDbContext>)_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
