using Microsoft.EntityFrameworkCore;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class UserAuthRepository : Repository<Users>, IUserAuthRepository
    {
        public UserAuthRepository(ApplicationDbContext context, IUriService uriservice) : base(context, uriservice) { }

        public async Task<Users> GetLoginByCredentials(RequestLoginModelDto login)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == login.Email && x.IsDeleted == false);
        }

        public async Task<Users> GetUserAuthenticated(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email && x.IsDeleted == false);
        }
    }
}
