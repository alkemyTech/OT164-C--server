using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationsPublicDTO
    {
        public string Name { get; set; }

        public string Image { get; set; }



        public string Address { get; set; }

        public int? Phone { get; set; }

        public string facebookUrl { get; set; }

        public string instagramUrl { get; set; }

        public string linkedinUrl { get; set; }

        [JsonIgnore]
        public List<Slides> Slides { get; set; }

    }
}
