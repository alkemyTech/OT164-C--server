using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ResponseOrganizationsDetailDto
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
    }
}
