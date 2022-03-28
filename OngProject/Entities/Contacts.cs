using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Contacts:EntityBase
    {
       
        [Required]
        public string name { get; set; }

        [Required]
        public string phone { get; set; }

        [EmailAddress]
        public string email { get; set; }

        public string message { get; set; }
    }
}
