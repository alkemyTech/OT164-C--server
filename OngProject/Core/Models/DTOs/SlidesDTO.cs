using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OngProject.Core.Models.DTOs
{
    public class SlidesDTO
    {
        [Required] 
        public string Image { set; get; }
        [Required]
        public string Text { set; get; }
        [Required]
        public int Orden { set; get; }
        [Required]
        public int OrganizationsId { set; get; }
    }
}
