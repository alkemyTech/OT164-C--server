using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRolesBusiness _RoleBusiness;

        public RolesController(IRolesBusiness roleBusiness)
        {
            _RoleBusiness = roleBusiness;
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            throw new NotImplementedException();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }



        [HttpPost]
        public async Task<ActionResult> Insert(Roles rol)
        {
            throw new NotImplementedException();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }


        [HttpPut]
        public async Task<ActionResult> Update(Roles rol)
        {
            throw new NotImplementedException();
        }

    }
}
