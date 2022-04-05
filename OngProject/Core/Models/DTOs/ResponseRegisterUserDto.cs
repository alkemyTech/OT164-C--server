using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ResponseRegisterUserDto
    {
        public string Token { get; set; }
        public Users User { get; set; }
    }
}
