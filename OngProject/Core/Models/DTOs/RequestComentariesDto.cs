using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class RequestComentariesDto
    {
        [Required(ErrorMessage = "El id de la noticia es requerido")]
        public int NewsId { get; set; }

        [Required(ErrorMessage = "El id del usuario es requerido")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El body es requerido")]
        public string Body { get; set; }
    }
}
