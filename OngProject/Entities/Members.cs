using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Members : EntityBase
    {


        [Required]
        [MaxLength(255)]
        public string name { get; set; }

        public string facebookUrl { get; set; }

        public string instagramUrl { get; set; }

        public string lindedinUrl { get; set; }

        [Required]
        [MaxLength(255)]
        public string image { get; set; }
     
        public string description { get; set; }



    }
}
