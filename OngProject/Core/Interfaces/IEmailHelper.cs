using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEmailHelper
    {
        public Task SendEmail(string email, string titulo, string texto);
    }
}
