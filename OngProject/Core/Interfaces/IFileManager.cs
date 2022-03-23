using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IFileManager
    {
        Task<string> EditFile(byte[] content, string extension, string container, string route,
            string contentType);
        Task DeleteFile(string ruta, string contenedor);
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}
