using Amazon.S3;
using Amazon.S3.Model;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class AlmacenadorArchivosAmazonS3 : IAlmacenadorArchivos
    {
        private readonly IAmazonS3 servicioAmazon;

        public AlmacenadorArchivosAmazonS3(IAmazonS3 servicioAmazon)
        {
            this.servicioAmazon = servicioAmazon;
        }
        public Task BorrarArchivo(string ruta, string contenedor)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {

            throw new NotImplementedException();
        
        }
    }
}
