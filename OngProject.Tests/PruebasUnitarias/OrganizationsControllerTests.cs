using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.PruebasUnitarias
{
    [TestClass]
    public class OrganizationsControllerTests : BasePruebas
    {
        [TestMethod]
        public async Task ObtenerTodasLasOrganizaciones()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);

            var slides1 =new List<Slides>();
            var slides2 =new List<Slides>();
            slides1.Add(new Slides() { Id = 1, image = "Image.png", orden = "1" });
            slides2.Add(new Slides() { Id = 2, image = "Image.png", orden = "1" });
            await unitOfWork.OrganizationsRepository.Insert(new Organizations { Id = 1 , Image = "image.png" , Email = "email@example.com" , WelcomeText = "Welcome" , Slides = slides1});
            await unitOfWork.OrganizationsRepository.Insert(new Organizations { Id = 2 , Image = "image2.png" , Email = "email2@example.com" , WelcomeText = "Welcome2", Slides = slides2 });
            await unitOfWork.SaveChangesAsync();

            //Prueba
       
            var business = new OrganizationsBusiness(unitOfWork);
            var controller = new OrganizationsController(business);

            var actionResult = await controller.GetPublic();

            var okResult = actionResult as ObjectResult;

            var data = okResult.Value as List<OrganizationsPublicDTO>;
            //Verificación
            Assert.AreEqual(2, data.Count);

        }



        [TestMethod]
        public async Task ActualizarOrganizacion()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);

            var slides1 = new List<Slides>();
            slides1.Add(new Slides() { Id = 1, image = "Image.png", orden = "1" });
            await unitOfWork.OrganizationsRepository.Insert(new Organizations { Name = "Name", Id = 1, Image = "image.png", Email = "email@example.com", WelcomeText = "Welcome", Slides = slides1 });
            await unitOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new OrganizationsBusiness(unit2);
            var controller = new OrganizationsController(business);

            var userUpdateDto = new OrganizationsUpdateDTO() { Name = "NewName", Image = "NewImage.png", Email = "newEmail@example.com", WelcomeText = "New Welcome", AboutUSText= "a", Address = "b" ,Facebook = "c", Instagram = "d", Linkedin = "e" , Phone = 2 };
            var id = 1;

            var respuesta = await controller.PutOrganization(id, userUpdateDto);
            var okResult = respuesta as ObjectResult;



            //Verificación
            Assert.AreEqual(200, okResult.StatusCode);

            var existe = unitOfWork.OrganizationsRepository.EntityExist(id);


            Assert.IsTrue(existe);

        }

        [TestMethod]
        public async Task ActualizarIdNoExistenteOrganizacion404()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unitOfWork = ConstruirUnitOfWork(nameDB);

            var slides1 = new List<Slides>();
            slides1.Add(new Slides() { Id = 1, image = "Image.png", orden = "1" });
            await unitOfWork.OrganizationsRepository.Insert(new Organizations { Name = "Name", Id = 1, Image = "image.png", Email = "email@example.com", WelcomeText = "Welcome", Slides = slides1 });
            await unitOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new OrganizationsBusiness(unit2);
            var controller = new OrganizationsController(business);

            var userUpdateDto = new OrganizationsUpdateDTO() { Name = "NewName", Image = "NewImage.png", Email = "newEmail@example.com", WelcomeText = "New Welcome", AboutUSText = "a", Address = "b", Facebook = "c", Instagram = "d", Linkedin = "e", Phone = 2 };
            var id = 2;

            var respuesta = await controller.PutOrganization(id, userUpdateDto);
            var Result = respuesta as ObjectResult;



            //Verificación
            Assert.AreEqual(404, Result.StatusCode);

        }




    }
}
