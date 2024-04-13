using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.Helpers.Concrete;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.UI.Areas.Customer.Models;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    
    [Area("Customer")]
    [Authorize]
    public class PTaskController : Controller
    {
        private readonly IPTaskService _pTaskManager;
        private readonly ITeamMemberService _teamMemberManager;
        private readonly UserManager<User> _userManager;
        private readonly IProjectService _projectManager;
        private readonly ISweetAlertService _sweetAlert;
        private readonly IImageService _imageManager;
        private readonly IPTaskFileService _fileManager;

        public PTaskController(IPTaskService pTaskManager, ITeamMemberService teamMemberManager, UserManager<User> userManager, IProjectService projectManager, ISweetAlertService sweetAlert, IImageService imageManager, IPTaskFileService fileManager)
        {
            _pTaskManager = pTaskManager;
            _teamMemberManager = teamMemberManager;
            _userManager = userManager;
            _projectManager = projectManager;
            _sweetAlert = sweetAlert;
            _imageManager = imageManager;
            _fileManager = fileManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var lowTaskResponse = await _pTaskManager.GetTasksByPriorityAsync(userId, Priority.Low);
            var mediumTaskResponse = await _pTaskManager.GetTasksByPriorityAsync(userId, Priority.Medium);
            var highTaskResponse = await _pTaskManager.GetTasksByPriorityAsync(userId, Priority.High);
            var criticalTaskResponse = await _pTaskManager.GetTasksByPriorityAsync(userId, Priority.Critical);
            var result = new CustomerPTaskListViewModel 
            {
                Critical = criticalTaskResponse.Data,
                High = highTaskResponse.Data,
                Low = lowTaskResponse.Data,
                Medium = mediumTaskResponse.Data,
            };
            return View(result);
        }
        public async Task<IActionResult> CompletedTasks()
        {
            var userId = _userManager.GetUserId(User);
            var pTasksResponse = await _pTaskManager.GetTasksByStatusAsync(userId, Status.Done);
            var result = pTasksResponse.Data.Select(x=> new CustomerCompletedPTaskViewModel
            {
                Id=x.Id,
                Name=x.Name,
                Description=x.Description,
                UserId=x.UserId,
                UserName=x.UserName,
                ProjectId=x.ProjectId,
                DueDate=x.DueDate,
                Priority=x.Priority,
                Status=x.Status,
            }).ToList();
            foreach(var task in result)
            {
                var projectResponse = await _projectManager.GetByIdAsync(task.ProjectId);
                task.ProjectName = projectResponse.Data.Name;
            }
            return View(result);
        }
        public async Task<IActionResult> AddTask(int projectId)
        {
            var addTaskViewModel = new AddPTaskViewModel { ProjectId = projectId,};
            var teamMembersResponse = await _teamMemberManager.GetTeamMembersByProjectIdAsync(projectId);
            var teamMemberList = teamMembersResponse.Data.Select(x=>new SelectListItem
            {
                Text = x.UserName,
                Value= x.UserId
            }).ToList();
            var result = new AssignPTaskToTeamMemberViewModel
            {
                AddPTaskViewModel = addTaskViewModel,
                TeamMembers = teamMemberList
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(AssignPTaskToTeamMemberViewModel assignPTaskToTeamMemberViewModel)
        {
            var teamMembersResponse = await _teamMemberManager.GetTeamMembersByProjectIdAsync(assignPTaskToTeamMemberViewModel.AddPTaskViewModel.ProjectId);
            var teamMemberList = teamMembersResponse.Data.Select(x => new SelectListItem
            {
                Text = x.UserName,
                Value = x.UserId
            }).ToList();
            assignPTaskToTeamMemberViewModel.TeamMembers = teamMemberList;
            if (!ModelState.IsValid)
            {
                TempData["AddTaskToast"] = _sweetAlert.MiddleNotification("warning", "Lütfen bilgileri kontrol ediniz.");
                return View(assignPTaskToTeamMemberViewModel);
            }
            var createdTask = await _pTaskManager.CreateAsync(assignPTaskToTeamMemberViewModel.AddPTaskViewModel);
            if (createdTask.IsSucceeded)
            {
                TempData["AddTaskToast"] = _sweetAlert.TopEndNotification("success", "Görev başarıyla eklendi.");
                return Redirect($"/Customer/Project/Detail?projectId={assignPTaskToTeamMemberViewModel.AddPTaskViewModel.ProjectId}");
            }
            TempData["AddTaskToast"] = _sweetAlert.MiddleNotification("error", "Görev eklenemedi.");
            ModelState.AddModelError("", "Görev atanırken bir sorun oluştu.");
            return View(assignPTaskToTeamMemberViewModel);
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
        public async Task<IActionResult> Edit(int pTaskId)
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
            if(files!=null && files.Count > 0)
            {
                foreach(var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileUrl = await _imageManager.UploadImage(file, FolderName.PTasks);
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
        public async Task<IActionResult> Done(int pTaskId)
        {
            await _pTaskManager.ChangePTaskStatusAsync(pTaskId, Status.Done);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> OnHold(int pTaskId)
        {
            var pTaskResponse = await _pTaskManager.GetByIdAsync(pTaskId);
            if (pTaskResponse.Data.Status == Status.OnHold)
            {
                await _pTaskManager.ChangePTaskStatusAsync(pTaskId, Status.Continues);
            }
            else
            {
                await _pTaskManager.ChangePTaskStatusAsync(pTaskId, Status.OnHold);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Stuck(int pTaskId)
        {
            var pTaskResponse = await _pTaskManager.GetByIdAsync(pTaskId);
            if (pTaskResponse.Data.Status == Status.Stuck)
            {
                await _pTaskManager.ChangePTaskStatusAsync(pTaskId, Status.Continues);
            }
            else
            {
                await _pTaskManager.ChangePTaskStatusAsync(pTaskId, Status.Stuck);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int pTaskId)
        {
            var response = await _pTaskManager.GetByIdAsync(pTaskId);
            var projectId = response.Data.ProjectId;
            await _pTaskManager.HardDeleteAsync(pTaskId);
            return Redirect($"/Customer/Project/Detail?projectId={projectId}");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int pTaskId, Status status)
        {
            await _pTaskManager.ChangePTaskStatusAsync(pTaskId, status);
            string icon = "success";
            string title = $"Durum \"{status.GetDisplayName()}\" olarak güncellendi";
            return Json(new { success = true, icon, title });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePriority(int pTaskId, Priority priority)
        {
            await _pTaskManager.ChangePTaskPriorityAsync(pTaskId, priority);
            string icon = "success";
            string title = $"Öncelik \"{priority.GetDisplayName()}\" olarak güncellendi";
            return Json(new { success = true, icon, title });
        }
        
    }
}
