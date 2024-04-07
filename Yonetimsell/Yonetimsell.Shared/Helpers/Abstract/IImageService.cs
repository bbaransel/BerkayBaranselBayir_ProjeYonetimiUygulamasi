using Microsoft.AspNetCore.Http;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.Helpers.Abstract
{
    public interface IImageService
    {
        bool ImageIsValid(string extension);
        Task<string> UploadImage(IFormFile image, FolderName folderName);
    }
}
