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
    public class FileManagerAmazonS3 : IFileManager
    {
        private readonly IAmazonS3 amazonService;

        public FileManagerAmazonS3(IAmazonS3 amazonService)
        {
            this.amazonService = amazonService;
        }
        public Task DeleteFile(string route, string container)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {

            Stream stream = new MemoryStream(content);
            string BucketName = "NAMEOFBUCKET";
            var nameFile = $"{Guid.NewGuid()}{extension}";


            var putRequest = new PutObjectRequest()
            {

                BucketName = BucketName,
                Key = nameFile,
                InputStream = stream,
                ContentType = contentType
            };

            var result = await this.amazonService.PutObjectAsync(putRequest);



            var resultURI = string.Format("http://{0}.s3.amazonaws.com/{1}", BucketName, nameFile);

            return resultURI;
        }
    }
}
