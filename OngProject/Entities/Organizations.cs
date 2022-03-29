using OngProject.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Organizations : EntityBase
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Image { get; set; }

        [DataType(DataType.Text)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[Null]")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int? Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(320)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(500)]
        public string WelcomeText { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(2000)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[Null]")]
        public string AboutUsText { get; set; }

        public string facebookUrl { get; set; }

        public string instagramUrl { get; set; }

        public string linkedinUrl { get; set; }
    
        public List<Slides> Slides { get; set; }


    }
}

