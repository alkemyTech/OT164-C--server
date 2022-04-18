using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Models;
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
    public class ActivitiesControllerTest: BasePruebas
    {




        //TEST DE UPDATE
        [TestMethod]
        public async Task ActualizarActividades()
        {
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);


            await unitOfWork.ActivitiesRepository.Insert(new Entities.Activities() { Name = "test", Content = "contenttest", Image = "test.png" });
            await unitOfWork.SaveChangesAsync();

            //Pruebas

            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new ActivitiesBusiness(unit2);
            var controller = new ActivitiesController(business, null);

            var ActivitiesUpdate = new ActivitiesDTO() { Name = "testNuevo", Content = "contentNuevotest", Image = "imgtest.png" };
            var id = 1;

            var respuesta = await controller.Put(ActivitiesUpdate, id);

            var okResult = respuesta as ObjectResult;

            //Verificacion

            Assert.AreEqual(200, okResult.StatusCode);


            var unit3 = ConstruirUnitOfWork(nameDB);
            business = new ActivitiesBusiness(unit3);
            var existe = unit3.ActivitiesRepository.EntityExist(id);


            Assert.IsTrue(existe);

        }


        //TEST DE GET ALL
        [TestMethod]
        public async Task ObtenerTodasLasActividades()
        {
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);


            await unitOfWork.ActivitiesRepository.Insert(new Entities.Activities() { Name = "test", Content = "contenttest", Image = "test.png" });
            await unitOfWork.ActivitiesRepository.Insert(new Entities.Activities() { Name = "testdos", Content = "contenttestdos", Image = "testods.png" });

            await unitOfWork.SaveChangesAsync();

            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new ActivitiesBusiness(unit2);
            var controller = new ActivitiesController(business, null);


            var actionResult = await controller.GetAll();

            var okResult = actionResult as ObjectResult;

            var data = okResult.Value as List<ActivitiesDTO>;

            Assert.AreEqual(2, data.Count);

        }



        //Test GetById
        [TestMethod]
        public async Task ObtenerActividadPorIdExistente()
        {
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);


            await unitOfWork.ActivitiesRepository.Insert(new Entities.Activities() { Name = "test", Content = "contenttest", Image = "test.png" });
            await unitOfWork.SaveChangesAsync();

            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new ActivitiesBusiness(unit2);
            var controller = new ActivitiesController(business, null);

            var query = await controller.GetById(1);

            var okResult = query as ObjectResult;

            var data = okResult.Value as ActivitiesDTO;

            var name = "test";

            Assert.IsNotNull(data);
            Assert.AreEqual(name, data.Name);
            Assert.AreEqual(200, okResult.StatusCode);


        }


        //Test GetById, Id incorrecto

        [TestMethod]
        public async Task ObtenerActividadNotFoundPorIdNoExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);

        
            var business = new ActivitiesBusiness(unifOfWork);
            var controller = new ActivitiesController(business, null);

            var query = await controller.GetById(1);

            var Result = query as ObjectResult;

            Assert.AreEqual(404, Result.StatusCode);
        }


        //TEST POST
        [TestMethod]
        public async Task IngresarNuevaActividad()
        {
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);



            ActivitiesCreationDTO actividad = new ActivitiesCreationDTO() { Name = "test", Content = "testContent" }; 

            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new ActivitiesBusiness(unit2);
            var controller = new ActivitiesController(business, null);


            var actionResult = controller.Insert(actividad);
            var okResult = actionResult.Result as ObjectResult;
            var value = okResult.Value as Response<ActivitiesGetDto>;
            var data = value.Data as ActivitiesGetDto;

            Assert.IsNotNull(data);



        }

        //TEST DELETE


        [TestMethod]
        public async Task BorrarActividad()
        {

            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);


            await unitOfWork.ActivitiesRepository.Insert(new Entities.Activities() { Name = "test", Content = "contenttest", Image = "test.png" });

            await unitOfWork.SaveChangesAsync();

            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new ActivitiesBusiness(unit2);
            var controller = new ActivitiesController(business, null);

            await controller.Delete(1);

            var actionResult = await controller.GetById(1);

            var okResult = actionResult as ObjectResult;
            var data = okResult.Value as ActivitiesDTO;

            Assert.IsNull(data);

        }
        }
}
