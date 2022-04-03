using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        void GetAllNews();
        void GetNewsById(int id);
        void CreateNews();
        void DeleteNews();
        Task<NewsDTO> UpdateNews(NewsDTO newsDTO, int id);

    }
}
