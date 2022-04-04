using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonialsCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { set; get; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { set; get; }
    }
}
