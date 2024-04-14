using Microsoft.AspNetCore.Http;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.Helpers.Abstract
{
    public interface IUploadService
    {
        bool FileIsValid(string extension);
        Task<string> UploadFile(IFormFile image, FolderName folderName);
    }
}
