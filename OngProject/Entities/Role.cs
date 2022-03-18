using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Role: EntityBase
    {

        [Required]
        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

    }
}
