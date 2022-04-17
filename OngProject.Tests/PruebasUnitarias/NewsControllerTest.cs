using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.PruebasUnitarias
{
    [TestClass]
    public class NewsControllerTest : BasePruebas
    {
        private NewsBusiness _newsBusiness;
        private UnitOfWork unitOfWork;
        private NewsController _newsController;
        private Mock<IHttpContextAccessor> httpContextAccessor;

        [TestInitialize]
        public void TestInitialization()
        {
            var nameDB = Guid.NewGuid().ToString();
            unitOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
            _newsBusiness = new NewsBusiness(unitOfWork, httpContextAccessor.Object);
            _newsController = new NewsController(_newsBusiness);
           
        }

        [TestMethod]
        public async Task GetAllNewsShouldResponseOk()
        {
            // Arrange
           
            for (int i = 0; i < 11; i++)
            {
                await unitOfWork.NewsRepository.Insert(new Entities.News() { Name = "asd", Content = "asd", Image = "asd", CategoriesId = 1 });
                await unitOfWork.SaveChangesAsync();
            }

            Filtros filtros = new Filtros();
            // Act
            var actionResult = await _newsBusiness.GetAllNews(filtros);
            var data = actionResult.Data;

            // Assert
            Assert.AreEqual(10, data.Count);
        }

        [TestMethod]
        public async Task GetByIdFailedIdNoExistShouldResponseNotFound()
        {
            // Arrange
            int newsId = 1;
            // Act
            var response = await _newsController.GetNewsById(newsId);
            var okResult = response as ObjectResult;

            // Assert
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetByIdShouldResponseResponseOk()
        {
            // Arrange
            News news = new News() { Id = 1, Name = "asdById", Content = "asdById", Image = "asdById", CategoriesId = 1 };
            await unitOfWork.NewsRepository.Insert(news);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await _newsController.GetNewsById(1);
            var okResult = response as ObjectResult;
            var data = okResult.Value as NewsGetByIdDTO;

            // Assert
            var name = "asdById";
            Assert.IsNotNull(data);
            Assert.AreEqual(name, data.Name);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetCommentsFromNewShouldResponseOk()
        {
            // Arrange
            int idNews = 1;
            for (int i = 0; i < 3; i++)
            {
                await unitOfWork.CommentsRepository.Insert(new Comentaries() { Body = "asd", NewsId = idNews });
                await unitOfWork.SaveChangesAsync();

            }

            // Act
            var response = await _newsController.GetCommentsFromNew(idNews);
            var okResult = response as ObjectResult;
            var data = okResult.Value as List<ComentariesFromNewsDTO>;
            
            // Assert
            Assert.AreEqual(3, data.Count);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task GetCommentsFromNewShouldResponseNoContent()
        {
            // Arrange
            int idNews = 45;

            // Act
            var response = await _newsController.GetCommentsFromNew(idNews);
            var result = response as ObjectResult;

            // Assert
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public async Task PostNewsShouldReturnOk()
        {
            // Arrange
            NewsDTO newsDTO = new NewsDTO() { Name = "asd", Content = "asd", Image = "asd", CategoriesId = 1 };

            // Act
            var response = await _newsController.PostNews(newsDTO);
            var result = response as ObjectResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task UpdateNewsShouldReturnNotFound()
        {
            // Arrange
            int newsId = 15;
            NewsDTO newsDTO = new NewsDTO() { Name = "asd", Content = "asd", Image = "asd", CategoriesId = 1 };
            await unitOfWork.NewsRepository.Insert(new News() { Content = "aaa", Name = "aaa", Image = "aaa", CategoriesId = 1 });
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await _newsController.PutNews(newsDTO, newsId);
            var result = response as ObjectResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteShouldReturnNotFound()
        {
            // Arrange
            int newsId = 15;
            NewsDTO newsDTO = new NewsDTO() { Name = "asd", Content = "asd", Image = "asd", CategoriesId = 1 };
            await unitOfWork.NewsRepository.Insert(new News() { Content = "aaa", Name = "aaa", Image = "aaa", CategoriesId = 1 });
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await _newsController.DeleteNews(newsId);
            var result = response as ObjectResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);

        }

    }
}
