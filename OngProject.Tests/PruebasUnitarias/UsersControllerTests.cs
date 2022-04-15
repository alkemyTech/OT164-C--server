using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.PruebasUnitarias
{
    [TestClass]
    public class UsersControllerTests:BasePruebas
    {
        [TestMethod]
        public async Task ObtenerTodosLosUsuarios()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
        
           
            await unifOfWork.UsersRepository.Insert(new Entities.Users() { FirstName = "asd", Email = "dsa", LastName = "sd", Password = "ddd", RolesId = 1 });
            await unifOfWork.UsersRepository.Insert(new Entities.Users() { FirstName = "asd", Email = "dsa", LastName = "sd", Password = "ddd", RolesId = 1 });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new UsersBusiness(unit2,null);
            var controller = new UsersController(business);

            var actionResult = await controller.GetAllAsync();

            var okResult = actionResult as ObjectResult;

            var data = okResult.Value as List<UserDTO>;
            //Verificación
            Assert.AreEqual(2, data.Count);

        }

        [TestMethod]
        public async Task ObtenerUsuarioPorIdNoExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB); 
           
            var business = new UsersBusiness(unifOfWork, null);
            
            //Prueba
            var controller = new UsersController(business);

            var respuesta = await controller.GetById(1);

            var okResult = respuesta as ObjectResult;
            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);
           
        }

        [TestMethod]
        public async Task ObtenerUsuarioPorIdExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
          
            await unifOfWork.UsersRepository.Insert(new Entities.Users() { FirstName = "asd", Email = "dsa", LastName = "sd", Password = "ddd", RolesId = 1 });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new UsersBusiness(unit2, null);
            var controller = new UsersController(business);

            var respuesta = await controller.GetById(1);

            var okResult = respuesta as ObjectResult;

            var data = okResult.Value as UserDTO;
            //Verificación
            var firstname = "asd";
            Assert.IsNotNull(data);
            Assert.AreEqual(firstname, data.FirstName);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [TestMethod]
        public async Task ActualizarUsuario()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);

            await unifOfWork.UsersRepository.Insert(new Entities.Users() { FirstName = "asd", Email = "dsa", LastName = "sd", Password = "ddd", RolesId = 1 });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new UsersBusiness(unit2, null);
            var controller = new UsersController(business);

            var userUpdateDto = new UserUpdateDTO() { FirstName = "nuevo usuario", LastName = "Nuevo", Password = "nueva contrasena" };
            var id = 1;

            var respuesta = await controller.Update(id, userUpdateDto);
            var okResult = respuesta as ObjectResult;


            //Verificación
            Assert.AreEqual(200, okResult.StatusCode);

            var unit3 = ConstruirUnitOfWork(nameDB);
            business = new UsersBusiness(unit3, null);
            var existe = unit3.UsersRepository.EntityExist(id);
            

            Assert.IsTrue(existe);

        }



        [TestMethod]
        public async Task IntentaBorrarUsuarioNoExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);

            var business = new UsersBusiness(unifOfWork, null);

            //Prueba
            var controller = new UsersController(business);

            var respuesta = await controller.Delete(1);

            var okResult = respuesta as ObjectResult;
            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);

        }


        [TestMethod]
        public async Task IntentaBorrarUsuarioExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);

            await unifOfWork.UsersRepository.Insert(new Entities.Users() { FirstName = "asd", Email = "dsa", LastName = "sd", Password = "ddd", RolesId = 1 });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new UsersBusiness(unit2, null);
            var controller = new UsersController(business);

            var respuesta = await controller.Delete(1);

            var okResult = respuesta as ObjectResult;


            //Verificación

            Assert.AreEqual(200, okResult.StatusCode);

            var unit3 = ConstruirUnitOfWork(nameDB);
            var existe = unit3.UsersRepository.GetById(1).Result;
            Assert.IsNull(existe);

        }


    }
}
