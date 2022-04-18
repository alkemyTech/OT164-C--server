using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.PruebasUnitarias
{
    [TestClass]
    public class ContactsControllerTests : BasePruebas
    {
        [TestMethod]
        public async Task IngresarNuevoContacto()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            var emailHelper = ConstruirEmailHelper();
            var contactsBusiness = new ContactsBusiness(unifOfWork, emailHelper);
            var contactsController = new ContactsController(contactsBusiness);
            ContactsGetDTO contactsGetDTO = new ContactsGetDTO() { Name = "Cesar Maydana", Email = "cesarmaydana@gmail.com", Phone = "3513945448", Message = "Hola mundo." };

            //Prueba
            var actionResult = contactsController.Insert(contactsGetDTO);
            var okResult = actionResult.Result as ObjectResult;
            var value = okResult.Value as Response<ContactsGetDTO>;
            var data = value.Data as ContactsGetDTO;

            //Verificación
            Assert.IsNotNull(value.Data);
        }

        [TestMethod]
        public async Task ObtenerTodosLosContactos()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            await unifOfWork.ContactsRepository.Insert(new Entities.Contacts() { name = "Cesar Maydana", phone = "111111", email = "cesarmaydana@gmail.com", message = "Hola a todos." });
            await unifOfWork.ContactsRepository.Insert(new Entities.Contacts() { name = "Leonardo Maydana", phone = "111111", email = "leonardomaydana@gmail.com", message = "Hola a todos." });
            await unifOfWork.ContactsRepository.Insert(new Entities.Contacts() { name = "Raul Maydana", phone = "111111", email = "raulmaydana@gmail.com", message = "Hola a todos." });
            await unifOfWork.SaveChangesAsync();
            int cant = 3;

            //Prueba
            var unifOfWork2 = ConstruirUnitOfWork(nameDB);
            var contactsBusiness = new ContactsBusiness(unifOfWork2, null);
            var contactsController = new ContactsController(contactsBusiness);
            var actionResult = await contactsController.GetAll();
            var okResult = actionResult as ObjectResult;
            var value = okResult.Value as Response<List<ContactsGetDTO>>;
            var data = value.Data as List<ContactsGetDTO>;

            //Verificación
            Assert.AreEqual(cant, data.Count);
        }

        [TestMethod]
        public async Task ObtenerNotFoundPorIdNoExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            var contactsBusiness = new ContactsBusiness(unifOfWork, null);

            //Prueba
            var controller = new ContactsController(contactsBusiness);
            var actionResult = await controller.GetById(1);
            var okResult = actionResult as ObjectResult;

            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [TestMethod]
        public async Task ObtenerContactoPorId()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            var contactsBusiness = new ContactsBusiness(unifOfWork, null);
            await unifOfWork.ContactsRepository.Insert(new Entities.Contacts() { name = "Raul Maydana", phone = "111111", email = "raulmaydana@gmail.com", message = "Hola a todos." });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unifOfWork2 = ConstruirUnitOfWork(nameDB);
            var contactsBusiness2 = new ContactsBusiness(unifOfWork2, null);
            var controller = new ContactsController(contactsBusiness2);
            var actionResult = await controller.GetById(1);
            var okResult = actionResult as ObjectResult;
            var value = okResult.Value as Response<ContactsGetDTO>;
            var data = value.Data as ContactsGetDTO;

            //Verificación
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public async Task ActualizarContacto()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            var contactsBusiness = new ContactsBusiness(unifOfWork, null);
            await unifOfWork.ContactsRepository.Insert(new Entities.Contacts() { name = "Raul Maydana", phone = "111111", email = "raulmaydana@gmail.com", message = "Hola a todos." });
            await unifOfWork.SaveChangesAsync();
            var respuesta = await unifOfWork.ContactsRepository.GetById(1);
            var fechaModificacion = respuesta.DateModified;

            //Prueba
            var unifOfWork2 = ConstruirUnitOfWork(nameDB);
            var contactsBusiness2 = new ContactsBusiness(unifOfWork2, null);
            var contactsController2 = new ContactsController(contactsBusiness2);
            var contactsGetDTO = new ContactsGetDTO() { Name = "Raul Maydana", Email = "maydanaraul@gmail.com", Phone = "3513945448", Message = "" };
            await contactsController2.Update(1, contactsGetDTO);
            await unifOfWork2.SaveChangesAsync();
            var respuesta2 = await unifOfWork2.ContactsRepository.GetById(1);
            var fechaModificacion2 = respuesta2.DateModified;

            //Verificación
            Assert.AreNotEqual(fechaModificacion, fechaModificacion2);
        }

        [TestMethod]
        public async Task BorrarContacto()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            await unifOfWork.ContactsRepository.Insert(new Entities.Contacts() { name = "Raul Maydana", phone = "111111", email = "raulmaydana@gmail.com", message = "Hola a todos." });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var contactsBusiness = new ContactsBusiness(unifOfWork, null);
            var contactsController = new ContactsController(contactsBusiness);
            await contactsController.Delete(1);

            //Verificación
            var actionResult = await contactsController.GetById(1);
            var okResult = actionResult as ObjectResult;
            var value = okResult.Value as Response<ContactsGetDTO>;
            var data = value.Data as ContactsGetDTO;
            Assert.IsNull(data);
        }

    }
}
