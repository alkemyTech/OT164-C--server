using Microsoft.EntityFrameworkCore;
using OngProject.Core.Mapper;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class CommentsRepository: Repository<Comentaries>, ICommentsRepository
    {
        private readonly EntityMapper mapper = new EntityMapper();
        public CommentsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<ComentariesFromNewsDTO>> GetComementsFromNew(int newId)
        {
            var comentaries = await _dbSet.Where(x => x.NewsId == newId && x.IsDeleted == false).ToListAsync();

            if (comentaries != null)
                return mapper.ToComentariesListDTO(comentaries);

            return null;
        }
    }
}
