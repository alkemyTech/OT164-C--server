using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class RequestUpdateMembersDto
    {
        public string Name { get; set; }

        public string FacebookUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string LinkedinUrl { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}
