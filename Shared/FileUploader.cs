using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared
{
    public class FileUploader
    {
        private readonly Cloudinary _cloudinary;
        public FileUploader()
        {
            Account acc = new Account(
                    "checkedincare",
                    "189224366899484",
                    "LBHBkmBOqTiXdvI6bZh28X8YWns"
                );

            _cloudinary = new Cloudinary(acc);
        }

        #region Methods
        public async Task<string> UploadFile(IFormFile image)
        {
            var results = new List<Dictionary<string, string>>();

            if (image.Length == 0) return null;

            var result = await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(image.FileName,
                    image.OpenReadStream()),
                Tags = "backend_photo_album"
            }).ConfigureAwait(false);

            return result.SecureUrl.AbsoluteUri;

        }

        #endregion
    }
}
