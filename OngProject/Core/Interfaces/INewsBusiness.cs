using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        void GetAllNews();
        void GetNewsById(int id);
        void CreateNews();
        void DeleteNews();
        void UpdateNews();
    }
}
