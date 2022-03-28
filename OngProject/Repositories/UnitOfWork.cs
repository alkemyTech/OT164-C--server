using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Categories> _categoriesRepository;
<<<<<<< Updated upstream
=======
        private readonly IRepository<Organizations> _organizationsRepository;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IRepository<Slides> _slidesRepository;
>>>>>>> Stashed changes
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IRepository<TestimonialsModel> TestimonialsModelRepository => _testimonialsModelRepository ?? new Repository<TestimonialsModel>(_context);
        public IRepository<News> NewsRepository =>  _newsRepository ?? new Repository<News>(_context);
        public IRepository<Users> UsersRepository => _usersRepository ?? new Repository<Users>(_context);
        public IRepository<Categories> CategoriesRepository => _categoriesRepository ?? new Repository<Categories>(_context);
<<<<<<< Updated upstream
=======
        public IRepository<Slides> SlidesRepository => _slidesRepository ?? new Repository<Slides>(_context);
        public IRepository<Organizations> OrganizationsRepository => _organizationsRepository ?? new Repository<Organizations>(_context);

>>>>>>> Stashed changes

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
