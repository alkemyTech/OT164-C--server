using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ResponseUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Token { get; set; }
    }
}
