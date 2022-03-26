using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ResponseLoginDto
    {
        public string Token { get; set; }
        public ResponseUserDto User { get; set; }
    }
}
