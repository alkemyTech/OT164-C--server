using Microsoft.EntityFrameworkCore;
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
    public class ActivitiesRepository : Repository<Activities>, IActivitiesRepository
    {
        private readonly IUriService uriservice;

        public ActivitiesRepository(ApplicationDbContext context, IUriService uriservice) : base(context,uriservice)
        {
            this.uriservice = uriservice;
        }
        public async Task<bool> GetByContent(string content)
        {
            var result = await _dbSet.AnyAsync(x => x.Content == content);
            return result;
        }

        public async Task<bool> GetByName(string name)
        {
            var result = await _dbSet.AnyAsync(x => x.Name == name);
            return result;
        }
    }
}
