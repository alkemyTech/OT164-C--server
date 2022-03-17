using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        //private readonly IRepository<TestimonialsModel> _testimonialsModelRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        //public IRepository<TestimonialsModel> TestimonialsModelRepository => _testimonialsModelRepository ?? new Repository<TestimonialsModel>(_context);
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
