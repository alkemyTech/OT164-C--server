using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class RequestLoginModelDto
    {
        [EmailAddress(ErrorMessage = "Formato de email invalido")]
        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}
