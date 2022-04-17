using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Tests.PruebasUnitarias
{
    [TestClass]
    public class MembersControllerTests : BasePruebas
    {
       private HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

        [TestMethod]
        public async Task ObtenerTodosLosMiembros()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            Filtros filtros = new Filtros();
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.MembersRepository.Insert(new Entities.Members()
            {
                Id = 1,
                name = "nombre del miembro",
                description = "description of member",
                image = "image of member",
                facebookUrl = "url facebook de miembros ",
                instagramUrl = "url instagram de miembros",
                lindedinUrl = "url linkedin de miembros"
            });
            await unifOfWork.MembersRepository.Insert(new Entities.Members()
            {
                Id = 2,
                name = "nombre del miembro",
                description = "description of member",
                image = "image of member",
                facebookUrl = "url facebook de miembros ",
                instagramUrl = "url instagram de miembros",
                lindedinUrl = "url linkedin de miembros"
            });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new MembersBusiness(unit2, httpContextAccessor);
            var controller = new MembersController(business, null);
            ActionResult<PagedResponse<List<MembersGetDTO>>> actionResult = await controller.GetAll(filtros);
            var okResult = actionResult.Result as ObjectResult;
            var data = okResult.Value as PagedResponse<List<MembersGetDTO>>;

            //Verificación
            Assert.AreEqual(2, data.TotalRecords);

        }

        [TestMethod]
        public async Task ObtenerMiembroPorIdNoExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            var business = new MembersBusiness(unifOfWork, httpContextAccessor);

            //Prueba
            var controller = new MembersController(business, null);
            var respuesta = await controller.GetById(1);
            var okResult = respuesta as ObjectResult;

            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);

        }

        [TestMethod]
        public async Task ObtenerMiembroPorIdExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.MembersRepository.Insert(new Entities.Members()
            {
                Id = 1,
                name = "nombre del miembro",
                description = "description of member",
                image = "image of member",
                facebookUrl = "url facebook de miembros ",
                instagramUrl = "url instagram de miembros",
                lindedinUrl = "url linkedin de miembros"
            });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new MembersBusiness(unit2, httpContextAccessor);
            var controller = new MembersController(business, null);
            var respuesta = await controller.GetById(1);
            var okResult = respuesta as ObjectResult;
            var data = okResult.Value as MembersGetDTO;
            //Verificación

            var name = "nombre del miembro";
            Assert.IsNotNull(data);
            Assert.AreEqual(name, data.Name);
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [TestMethod]
        public async Task ActualizarMember()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.MembersRepository.Insert(new Entities.Members()
            {
                Id = 1,
                name = "nombre de miembros",
                description = "description of member",
                image = "image of member",
                facebookUrl = "url facebook de miembros ",
                instagramUrl = "url instagram de miembros",
                lindedinUrl = "url linkedin de miembros"
            });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new MembersBusiness(unit2, httpContextAccessor);
            var controller = new MembersController(business,null);

            var userUpdateDto = new RequestUpdateMembersDto() {
                Name = "nuevo nombre de miembros",
                Description = "nuevo description del miembro",
                Image = "image of member",
                FacebookUrl = "url facebook de miembros ",
                InstagramUrl = "url instagram de miembros", 
                LinkedinUrl = "url linkedin de miembros"
            };
            var id = 1;
            var respuesta = await controller.Put(userUpdateDto,id);
          var okResult = respuesta as ObjectResult;

            //Verificación
            var unit3 = ConstruirUnitOfWork(nameDB);
            business = new MembersBusiness(unit3, httpContextAccessor);
            var existe = unit3.MembersRepository.EntityExist(id);
            Assert.IsTrue(existe);
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [TestMethod]
        public async Task IntentaBorrarMemberNoExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();

            var business = new MembersBusiness(unifOfWork, httpContextAccessor);

            //Prueba
            var controller = new MembersController(business,null);

            var respuesta = await controller.Delete(1);

            var okResult = respuesta as ObjectResult;
            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);
        }


        [TestMethod]
        public async Task IntentaBorrarMiembroExistente()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);

            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.MembersRepository.Insert(new Entities.Members()
            {
                Id = 1,
                name = "nombre de miembros",
                description = "description of member",
                image = "image of member",
                facebookUrl = "url facebook de miembros ",
                instagramUrl = "url instagram de miembros",
                lindedinUrl = "url linkedin de miembros"
            });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new MembersBusiness(unit2, httpContextAccessor);
            var controller = new MembersController(business,null);
            var respuesta = await controller.Delete(1);
            var okResult = respuesta as ObjectResult;

            //Verificación
            Assert.AreEqual(200, okResult.StatusCode);
            var unit3 = ConstruirUnitOfWork(nameDB);
            var existe = unit3.MembersRepository.GetById(1).Result;
            Assert.IsNull(existe);

        }





    }
}
