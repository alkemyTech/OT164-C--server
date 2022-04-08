using Microsoft.EntityFrameworkCore;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
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
        private readonly IUriService _uriService;

        private readonly IRepository<Activities> _activitiesRepository;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Categories> _categoriesRepository;
        private readonly IRepository<Organizations> _organizationsRepository;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IRepository<Slides> _slidesRepository;
        private readonly IRepository<Members> _membersRepository;
        private readonly IRepository<Comentaries> _comentariesRepository;
        private readonly IRepository<Contacts> _contactsRepository;
        private readonly IActivitiesRepository _CustomActivitiesRepository;
        private readonly IRepository<testimonials> _testimonialsRepository;

        public UnitOfWork(ApplicationDbContext context, IUriService uriservice)
        {
            _context = context;
            this._uriService = uriservice;
        }

        //public IRepository<TestimonialsModel> TestimonialsModelRepository => _testimonialsModelRepository ?? new Repository<TestimonialsModel>(_context);
        public IRepository<Activities> ActivitiesRepository => _activitiesRepository ?? new Repository<Activities>(_context, _uriService);
        public IRepository<News> NewsRepository =>  _newsRepository ?? new Repository<News>(_context, _uriService);
        public IRepository<Users> UsersRepository => _usersRepository ?? new Repository<Users>(_context, _uriService);
        public IUserAuthRepository UserAuthRepository => _userAuthRepository ?? new UserAuthRepository(_context, _uriService);
        public ICommentsRepository CommentsRepository => _commentsRepository ?? new CommentsRepository(_context, _uriService);

        public IActivitiesRepository CustomActivitiesRepository => _CustomActivitiesRepository ?? new ActivitiesRepository(_context, _uriService);
        public IRepository<Categories> CategoriesRepository => _categoriesRepository ?? new Repository<Categories>(_context, _uriService);

        public IRepository<Organizations> OrganizationsRepository => _organizationsRepository ?? new Repository<Organizations>(_context, _uriService);

        public IRepository<Slides> SlidesRepository => _slidesRepository ?? new Repository<Slides>(_context, _uriService);

        public IRepository<Members> MembersRepository => _membersRepository ?? new Repository<Members>(_context, _uriService);
        public IRepository<Comentaries> ComentariesRepository => _comentariesRepository ?? new Repository<Comentaries>(_context, _uriService);
        public IRepository<Contacts> ContactsRepository => _contactsRepository ?? new Repository<Contacts>(_context, _uriService);
        public IRepository<testimonials> TestimonialsRepository => _testimonialsRepository ?? new Repository<testimonials>(_context, _uriService);

        public DbSet<testimonials> testimonialsDbSet => _context.Testimonials;
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
