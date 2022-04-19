using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Models;
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
    public class TestimonialsControllerTest: BasePruebas
    {
        private readonly HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

        [TestMethod]
        public async Task Get_All_Testimonials()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            Filtros filtros = new();
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.TestimonialsRepository.Insert(new testimonials(){ name = "testimonial 1"});
            await unifOfWork.TestimonialsRepository.Insert(new testimonials(){ name = "testimonial 2"});
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new TestimonialsBusiness(unit2, null, httpContextAccessor);
            var controller = new TestimonialsController(business);
            ActionResult<PagedResponse<List<TestimonialsDTO>>> actionResult = await controller.GetAll(filtros);
            var okResult = actionResult.Result as ObjectResult;
            var data = okResult.Value as PagedResponse<List<TestimonialsDTO>>;

            //Verificación
            Assert.AreEqual(2, data.TotalRecords);
        }

        [TestMethod]
        public async Task Get_Testimonial_By_Wrong_Id()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            var business = new TestimonialsBusiness(unifOfWork, null, httpContextAccessor);

            //Prueba
            var controller = new TestimonialsController(business);
            var respuesta = await controller.GetById(1);
            var okResult = respuesta as ObjectResult;

            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);

        }

        [TestMethod]
        public async Task Get_Testimonial_By_Id_Successfully()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.TestimonialsRepository.Insert(new testimonials() { name = "testimonial 1"});
            await unifOfWork.TestimonialsRepository.Insert(new testimonials() { name = "testimonial 2" });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new TestimonialsBusiness(unit2, null, httpContextAccessor);
            var controller = new TestimonialsController(business);
            var respuesta = await controller.GetById(2);
            var okResult = respuesta as ObjectResult;
            var data = okResult.Value as TestimonialsDTO;
            //Verificación

            var name = "testimonial 2";
            Assert.IsNotNull(data);
            Assert.AreEqual(name, data.Name);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_Testimonial_Successfully()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            await unifOfWork.TestimonialsRepository.Insert(new testimonials(){name = "Testimonial 1", content = "content 1"});
            await unifOfWork.SaveChangesAsync();

            var testimonial1 = await unifOfWork.TestimonialsRepository.GetById(1);
            var dateModified1 = testimonial1.DateModified;

            //Prueba
            var unit2 = ConstruirUnitOfWork(nameDB);
            var business = new TestimonialsBusiness(unit2,null, httpContextAccessor);
            var controller = new TestimonialsController(business);

            var userUpdateDto = new TestimonialsPutDto()
            {
                Name = "Testimonial Edited",
                Content = "content edited"
            };

            var id = 1;
            await controller.Put(id, userUpdateDto);
            await unifOfWork.SaveChangesAsync();

            var testimonial2 = await unifOfWork.TestimonialsRepository.GetById(id);
            var dateModified2 = testimonial2.DateModified;

            //Verificación
            Assert.AreNotEqual(dateModified1, dateModified2);
        }

        [TestMethod]
        public async Task Delete_Nonexistent_Testimonial()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();

            var business = new TestimonialsBusiness(unifOfWork, null, httpContextAccessor);

            //Prueba
            var controller = new TestimonialsController(business);

            var respuesta = await controller.Delete(1);

            var okResult = respuesta as ObjectResult;
            //Verificación
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Testimonial_Successfully()
        {
            //Preparación
            var nameDB = Guid.NewGuid().ToString();
            var unifOfWork = ConstruirUnitOfWork(nameDB);
            httpContextAccessor.HttpContext = new DefaultHttpContext();


            await unifOfWork.TestimonialsRepository.Insert(new Entities.testimonials() { name = "Testimonial 1", content = "Content" });
            await unifOfWork.SaveChangesAsync();

            //Prueba
            var testimonialBusiness = new TestimonialsBusiness(unifOfWork, null, httpContextAccessor);
            var testimonialController = new TestimonialsController(testimonialBusiness);
            await testimonialController.Delete(1);

            //Verificación
            var actionResult = await testimonialController.GetById(1);
            var okResult = actionResult as ObjectResult;
            var value = okResult.Value as Response<TestimonialsDTO>;
            var data = value.Data as TestimonialsDTO;
            Assert.IsNull(data);
        }
    }
}
