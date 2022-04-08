using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<PagedResponse<List<T>>> GetAllPaged(IQueryable<T> collection, int pageNumber, int pageSize, Filtros filtros, HttpContext context);
        public Task<IEnumerable<T>> GetAllIncludeAsync(string entity);
        public Task<T> GetById(int id);

        public Task<T> GetByIdIncludeAsync(int id, string entity);

        public Task Insert(T entity);  
        public Task Delete(int id);
        public Task Update(T entity);
        public bool EntityExist(int id);

    }
}
