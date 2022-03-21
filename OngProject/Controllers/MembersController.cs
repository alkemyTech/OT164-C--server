﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Produces("application/json")]
    [Route("Members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberBusiness memberBusiness;

        public MembersController(IMemberBusiness _memberBusiness)
        {
            memberBusiness = _memberBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Members>>> Get()
        {
            return NoContent();
            //return Ok(await memberBusiness.GetAll());
        }

        [HttpGet("Id:int")]
        public async Task<ActionResult<Members>> GetById(int Id)
        {
            return NoContent();
           
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id)
        {
            return NoContent();
          
        }

        [HttpPost]
        public async Task<ActionResult> Post(Members member)
        {
            return NoContent();
           
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult> Delete(int Id)
        {
            return NoContent();
          
        }


    }
}