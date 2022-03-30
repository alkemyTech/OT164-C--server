using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UserDTO
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Photo { get; set; }
    }
}

