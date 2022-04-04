using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ActivitiesGetDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
