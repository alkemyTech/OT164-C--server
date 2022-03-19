using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Users : EntityBase
    {
      
        [Required]
        [Column(TypeName ="VARCHAR(250)")]
        [MaxLength(250)]
        public string FirstName { get; set; }

        [Required] 
        [Column(TypeName = "VARCHAR(250)")]
        [MaxLength(250)]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        [MaxLength(250)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(250)")]
        [MaxLength(250)]
        public string Password { get; set; }

        
        public string Photo { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Roles Roles { get; set; }
    }
}
