using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public FileManagerAmazonS3(IAmazonS3 amazonService, IConfiguration _configuration)
        {
            this.amazonService = amazonService;
            this.configuration = _configuration;

            var chain = new CredentialProfileStoreChain("App_data\\credentials.ini");
            AWSCredentials awsCredentials;
            // var newRegion = RegionEndpoint.GetBySystemName("us-west-new");
            if (chain.TryGetAWSCredentials("default", out awsCredentials))
            {
                this.amazonService = new AmazonS3Client(awsCredentials.GetCredentials().AccessKey, awsCredentials.GetCredentials().SecretKey, RegionEndpoint.USEast1);
            }
        }
        public async Task DeleteFileAsync(string route, string container)
        {
            throw new NotImplementedException();
        }

        public async Task<string> EditFileAsync(IFormFile content, string extension, string container, string route, string contentType)
        {
            throw new NotImplementedException();
        }

      

        public async Task<string> UploadFileAsync(IFormFile content, string extension, string container, string contentType)
        {

         
            string BucketName = configuration["AWS:BucketName"];
            var nameFile = $"{Guid.NewGuid()}{extension}";


            var putRequest = new PutObjectRequest()
            {

                BucketName = BucketName,
                Key = nameFile,
                InputStream = content.OpenReadStream(),
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };

            var result = await this.amazonService.PutObjectAsync(putRequest);

            var resultURI = string.Format("http://{0}.s3.amazonaws.com/{1}", BucketName, nameFile);

            return resultURI;
        }
    }
}
