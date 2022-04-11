using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        Task<PagedResponse<List<NewsDTO>>> GetAllNews(Filtros filtros);
        Response<NewsGetByIdDTO> GetNewsById(int id);
        Task<Response<NewsDTO>> CreateNews(NewsDTO news);
        Task<Response<string>> DeleteNews(int id);
        Task<NewsDTO> UpdateNews(NewsDTO newsDTO, int id);

    }
}
