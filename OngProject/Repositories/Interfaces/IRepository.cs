using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity GetById(int id);
        public void Insert(TEntity entity);
        public void Delete(int id);
        public void Update(TEntity entity);
    }
}
