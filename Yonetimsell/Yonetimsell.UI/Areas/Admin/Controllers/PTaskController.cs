using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PTaskController : Controller
    {
        private readonly IPTaskService _pTaskManager;
        private readonly IPTaskFileService _fileManager;
        private readonly ISweetAlertService _sweetAlert;
        private readonly IUploadService _uploadManager;

        public PTaskController(IPTaskService pTaskManager, IPTaskFileService fileManager, ISweetAlertService sweetAlert, IUploadService uploadManager)
        {
            _pTaskManager = pTaskManager;
            _fileManager = fileManager;
            _sweetAlert = sweetAlert;
            _uploadManager = uploadManager;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _pTaskManager.GetAllAsync();
            return View(response.Data);
        }
        public async Task<IActionResult> Detail(int pTaskId)
        {
            var pTaskResponse = await _pTaskManager.GetByIdAsync(pTaskId);
            var filesResponse = await _fileManager.GetAllByPTaskIdAsync(pTaskId);
            var result = new EditPTaskViewModel
            {
                UserId = pTaskResponse.Data.UserId,
                ProjectId = pTaskResponse.Data.ProjectId,
                Priority = pTaskResponse.Data.Priority,
                Name = pTaskResponse.Data.Name,
                Description = pTaskResponse.Data.Description,
                DueDate = pTaskResponse.Data.DueDate,
                Id = pTaskResponse.Data.Id,
                Status = pTaskResponse.Data.Status,
                UserName = pTaskResponse.Data.UserName,
                PTaskFiles = filesResponse.Data
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPTaskViewModel editPTaskViewModel, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                TempData["EditTaskToast"] = _sweetAlert.MiddleNotification("warning", "Lütfen bilgileri kontrol ediniz!");
                return View(editPTaskViewModel);
            }
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileUrl = await _uploadManager.UploadFile(file, FolderName.PTasks);
                    var pTaskFileViewModel = new PTaskFileViewModel
                    {
                        FileName = fileName,
                        FileUrl = fileUrl,
                        PTaskId = editPTaskViewModel.Id
                    };
                    var createdFileResponse = await _fileManager.CreateAsync(pTaskFileViewModel);
                    if (!createdFileResponse.IsSucceeded)
                    {
                        TempData["EditTaskToast"] = _sweetAlert.MiddleNotification("error", "Dosyalar yüklenemedi!");
                        return View(editPTaskViewModel);
                    }
                }
            }
            var response = await _pTaskManager.UpdateAsync(editPTaskViewModel);
            if (response.IsSucceeded)
            {
                TempData["EditTaskToast"] = _sweetAlert.MiddleNotification("success", "Görev başarıyla güncellendi");
                return RedirectToAction("Detail", "Project", new { projectId = editPTaskViewModel.ProjectId });
            }
            TempData["EditTaskToast"] = _sweetAlert.MiddleNotification("error", "Görev güncellenemedi tekrar deneyiniz.");
            return View(editPTaskViewModel);
        }
        public async Task<IActionResult> Remove(int pTaskId)
        {
            var response = await _pTaskManager.GetByIdAsync(pTaskId);
            var projectId = response.Data.ProjectId;
            await _pTaskManager.HardDeleteAsync(pTaskId);
            return RedirectToAction("Index");
        }
    }
}
