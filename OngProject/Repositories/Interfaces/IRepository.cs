using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetById(int id);
        public Task Insert(TEntity entity);
        public  Task Delete(int id);
        public Task Update(TEntity entity);
    }
}
