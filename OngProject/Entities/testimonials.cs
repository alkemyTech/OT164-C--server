using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public  class testimonials :EntityBase
    {


        [Required]
        [StringLength(50)]
        public string name { set; get; }

        [Required]
        [MaxLength(255)]
        public string image { set; get; }

        [Required]
        [StringLength(500)]
        public string content { set; get; }

        public DateTime deleteAt { get; set; }
    }
}
