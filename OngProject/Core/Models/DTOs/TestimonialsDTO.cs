using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonialsDTO
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Image { get; set; }

        public string Content { set; get; }
    }
}
