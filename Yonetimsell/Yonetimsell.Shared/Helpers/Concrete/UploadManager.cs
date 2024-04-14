using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.Helpers.Abstract;

namespace Yonetimsell.Shared.Helpers.Concrete
{
    public class UploadManager : IUploadService
    {
        private readonly string _filesFolder;
        public UploadManager(IWebHostEnvironment _env)
        {
            _filesFolder = Path.Combine(_env.WebRootPath, "files");
        }
        public bool FileIsValid(string extension)
        {
            string[] correctExtensions = { ".png", ".jpg", ".jpeg",".pdf",".txt",".docx" };
            if (correctExtensions.Contains(extension))
            {
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IFormFile file, FolderName folderName)
        {
            if (file == null)
            {
                return null;
            }
            var fileExtension = Path.GetExtension(file.FileName);
            if (!FileIsValid(fileExtension))
            {
                return "";
            }
            var targetFolder = Path.Combine(_filesFolder, folderName.GetDisplayName());
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var fileFullPath = Path.Combine(targetFolder, fileName);
            var result = $"/files/files/{folderName.GetDisplayName()}/{fileName}";
            await using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return result;

        }
    }
}
