using Microsoft.EntityFrameworkCore;
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
        public UserAuthRepository(ApplicationDbContext context) : base(context){ }

        public async Task<Users> GetLoginByCredentials(RequestLoginModelDto login)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == login.Email && x.IsDeleted == false);
        }
    }
}
