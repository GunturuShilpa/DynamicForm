using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.BlobContainer.Repoistory
{
    public interface IBlobContainerRepository
    {
        Task<BlobContainerInfo> CreateUserContainer(string userName);
        Task<string> CreateFileInPath(IFormFile file);
    }
}
