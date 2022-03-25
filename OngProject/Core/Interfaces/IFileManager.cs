using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IFileManager
    {
        Task<string> EditFileAsync(IFormFile content, string extension, string container, string route,
            string contentType);
        Task DeleteFileAsync(string ruta, string contenedor);
        Task<string> UploadFileAsync(IFormFile content, string extension, string container, string contentType);
    }
}
