using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.Helpers.Abstract;

namespace Yonetimsell.Shared.Helpers.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly string _imagesFolder;
        public ImageManager(IWebHostEnvironment _env)
        {
            _imagesFolder = Path.Combine(_env.WebRootPath, "files","images");
        }
        public bool ImageIsValid(string extension)
        {
            string[] correctExtensions = { ".png", ".jpg", ".jpeg" };
            if (correctExtensions.Contains(extension))
            {
                return true;
            }
            return false;
        }

        public async Task<string> UploadImage(IFormFile image, FolderName folderName)
        {
            if (image == null)
            {
                return null;
            }
            var fileExtension = Path.GetExtension(image.FileName);
            if (!ImageIsValid(fileExtension))
            {
                return "";
            }
            var targetFolder = Path.Combine(_imagesFolder, folderName.GetDisplayName());
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var fileFullPath = Path.Combine(targetFolder, fileName);
            var result = $"/files/images/{folderName.GetDisplayName()}/{fileName}";
            await using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return result;

        }
    }
}
