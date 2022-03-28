using OngProject.Core.Mapper;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface ICommentsRepository: IRepository<Comentaries>
    {
        Task<List<ComentariesFromNewsDTO>> GetComementsFromNew(int newId);
    }
}
