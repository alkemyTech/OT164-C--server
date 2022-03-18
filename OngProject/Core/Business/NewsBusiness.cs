using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public NewsBusiness(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateNews()
        {
            throw new NotImplementedException();
        }

        public void DeleteNews()
        {
            throw new NotImplementedException();
        }

        public void GetAllNews()
        {
            //_unitOfWork.NewsRepository.GetAll();
           throw new NotImplementedException();
        }

        public void GetNewsById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateNews()
        {
            throw new NotImplementedException();
        }
    }
}
