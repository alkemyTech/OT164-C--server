using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.DataAccess;
using OngProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests
{
    public class BasePruebas
    {
        protected ApplicationDbContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nombreDB).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;

            var dbContext = new ApplicationDbContext(opciones);
            return dbContext;
        }

        protected UnitOfWork ConstruirUnitOfWork(string nameDB)
        {
            var moq = new Mock<IUriService>();
            UnitOfWork unit = new UnitOfWork(ConstruirContext(nameDB), moq.Object);
            return unit;
        }

        protected EntityMapper ConstruirMapper()
        {
            return new EntityMapper();
        }

       protected EmailHelper ConstruirEmailHelper()
        {
            var moq = new Mock<IConfiguration>();
            moq.SetupGet(x => x[It.Is<string>(s => s == "EmailConfig:ApiKey")]).Returns("mock value");
            moq.SetupGet(x => x[It.Is<string>(s => s == "EmailConfig:Email")]).Returns("mock value");
            EmailHelper emailHelper = new EmailHelper(moq.Object);
            return emailHelper;
        }

        
    }
}
