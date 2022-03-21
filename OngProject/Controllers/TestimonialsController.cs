using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Data;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly OngProjectContext _context;

        public TestimonialsController(OngProjectContext context)
        {
            _context = context;
        }

      
        private bool testimonialsExists(int id)
        {
            return _context.testimonials.Any(e => e.id == id);
        }
    }
}
