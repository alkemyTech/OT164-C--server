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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
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
    }
}
