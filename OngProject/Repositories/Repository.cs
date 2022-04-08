
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IUriService uriservice;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context, IUriService uriservice)
        {
            _context = context;
            this.uriservice = uriservice;
            _dbSet = context.Set<TEntity>();
        }
        /*
        public Repository(IUnitOfWork unitOfWork)
            : this(unitOfWork.Context)
        {
        }
        */ 


        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync(string entity)
        {
            return await _dbSet.Include(entity).Where(x => x.IsDeleted == false).ToListAsync();
        }


        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }


        public async Task<TEntity> GetByIdIncludeAsync(int id ,string entity)
        {
            return await _dbSet.Include(entity).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }


        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                entityToDelete.IsDeleted = true;
                entityToDelete.DateModified = DateTime.Now;
            }
        }
        public async Task Update(TEntity entity)
        {
            entity.DateModified = DateTime.Now;
           _context.Entry(entity).State = EntityState.Modified;
            
        }

        public bool EntityExist(int id)
        {
            bool isEntityExist = _dbSet.Any(n => n.Id == id);
            return isEntityExist;
        }

        public async Task<PagedResponse<List<TEntity>>> GetAllPaged(IQueryable<TEntity> collection, int pageNumber, int pageSize, Filtros filtros, HttpContext context)
        {
            var xQueryable = _dbSet as IQueryable<TEntity>;
            double totalRecords = await xQueryable.CountAsync();
            int totalReg = await xQueryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(totalRecords / pageSize);
            context.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
            var xList = await xQueryable.Paginar(filtros.Paginacion).ToListAsync();
            var route = context.Request.Path.Value;
            var validFilter = new PaginationFilter(filtros.Pagina, pageSize);
            var pagedReponse = PaginationHelper.CreatePagedReponse<TEntity>(xList, validFilter, totalReg, uriservice, route);

            return pagedReponse;

        }
    }
}
