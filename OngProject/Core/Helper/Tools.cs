using Microsoft.AspNetCore.Http;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class Tools
    {
        private readonly IFormFile image;
       
        private readonly string container;
        private readonly IFileManager fileManager;

        public Tools(IFormFile image, string container, IFileManager fileManager)
        {
            this.image = image;
            this.container = container;
            this.fileManager = fileManager;
        }

        public async Task<Response<string>> EvaluateImage()
        {
            string result = "";
            IFileManager _fileManager;
            Response<string> response = new Response<string>();
            if (image != null)
            {
                try
                {
                    
                    var extension = Path.GetExtension(image.FileName);
                    result = await fileManager.UploadFileAsync(image, extension, container,
                    image.ContentType);
                    response.Data = result;
                    response.Succeeded = true;
                }
                catch (Exception e)
                {
                    response.Succeeded = false;
                    response.Message = e.Message;
                }

            }
            else
            {
                response.Succeeded = false;
                response.Message = "image it's empty.";
            }
            return response;
        }

    }
}
