using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.BlobContainer.Repoistory
{
    public class BlobContainerRepository : IBlobContainerRepository
    {
        private readonly IConfiguration _configuration;

        public BlobContainerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<BlobContainerInfo> CreateUserContainer(string userName)
        {            
            BlobContainerClient container = new(_configuration["BlobConnectionString"], userName.ToLower());
            var res = await container.CreateIfNotExistsAsync(PublicAccessType.Blob);
            return res;
        }

        public async Task<string> CreateFileInPath(IFormFile file)
        {
            BlobContainerClient container = new(_configuration["BlobConnectionString"], _configuration["BlobContainerName"]);
            container.CreateIfNotExists(PublicAccessType.Blob);
            string extension = Path.GetExtension(file.FileName);
            string newFileName = Guid.NewGuid() + extension;

            var blockBlob = container.GetBlobClient(_configuration["BlobFolderName"] + "/" + newFileName);
            if (!blockBlob.Exists())
            {
                var header = new BlobHttpHeaders();
                header.ContentType = file.ContentType;
                using var fileStream = file.OpenReadStream();
                var res = await blockBlob.UploadAsync(fileStream, header);
                if (res.GetRawResponse().Status == 201 && res.GetRawResponse().ReasonPhrase == "Created")
                {
                    return blockBlob.Uri.AbsoluteUri;
                }
            }
            return blockBlob.Uri.AbsoluteUri;// "File already exits." 
        }
       
    }
}
