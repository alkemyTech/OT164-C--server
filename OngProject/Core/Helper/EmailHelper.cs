using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _configuration;
        private const string templatePath = @"Templates\htmlpage.html";

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string email, string titulo, string texto)
        {

            string dir = Path.Combine(Directory.GetCurrentDirectory(), templatePath);
            string template =  File.ReadAllText(dir);
            template =  template.Replace("{titulo}", titulo);
            template =  template.Replace("{texto}", texto);

            var client = new SendGridClient(_configuration["EmailConfig:ApiKey"]);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_configuration["EmailConfig:Email"]),
                Subject = titulo,
                HtmlContent = template
            };
            msg.AddTo(email);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
