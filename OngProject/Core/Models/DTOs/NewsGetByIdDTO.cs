using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class NewsGetByIdDTO
    {
        

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Image { get; set; }

        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }


        [Required]
        public DateTime DateModified { get; set; }

        [Required]

        public bool IsDeleted { get; set; }



    }
}
