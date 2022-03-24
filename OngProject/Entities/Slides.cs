using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OngProject.Entities
{
    public class Slides:EntityBase
    {
     [Required]
     [MaxLength(200)]
     public string image { set; get; }
     [Required]
     public string text { set; get; }
     [Required]
     public string orden { set; get; }
     [Required]
     public int idOrganizations { set; get; }

    }


}
