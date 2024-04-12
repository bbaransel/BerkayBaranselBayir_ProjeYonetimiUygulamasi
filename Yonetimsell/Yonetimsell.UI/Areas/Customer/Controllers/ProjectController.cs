using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.Net.NetworkInformation;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;
using Yonetimsell.UI.Areas.Customer.Models;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProjectService _projectManager;
        private readonly ITeamMemberService _teamMemberManager;
        private readonly IPTaskService _pTaskManager;
        private readonly MapperlyConfiguration _mapperly;
        private readonly ISweetAlertService _sweetAlert;

        public ProjectController(UserManager<User> userManager, IProjectService projectManager, ITeamMemberService teamMemberManager, IPTaskService pTaskManager, MapperlyConfiguration mapperly, ISweetAlertService sweetAlert)
        {
            _userManager = userManager;
            _projectManager = projectManager;
            _teamMemberManager = teamMemberManager;
            _pTaskManager = pTaskManager;
            _mapperly = mapperly;
            _sweetAlert = sweetAlert;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var projectsResponse = await _projectManager.GetProjectsByUserIdAsync(userId);
            if (!projectsResponse.IsSucceeded)
            {
                return Redirect("/Customer");
            }
            
            var projectsResult = projectsResponse.Data.Select(x => new CustomerProjectViewModel
            {
                Budget = x.Budget,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                EndDate = x.EndDate,
                Id = x.Id,
                Name = x.Name,
                Priority = x.Priority,
                PTasks = x.PTasks,
                Status = x.Status,
                Subscriptions = x.Subscriptions,
                User = x.User,
                UserId = x.UserId,
            }).ToList();
            foreach(var p in projectsResult)
            {
                int percentage = await _pTaskManager.GetPTaskProgressPercentageByProjectIdAsync(p.Id);
                p.ProgressPTaskPercentage = percentage;
                var teamMembersResponse = await _teamMemberManager.GetTeamMembersByProjectIdAsync(p.Id);
                var teamMembersSelectList = teamMembersResponse.Data.Select(x=> new SelectListItem
                {
                    Text = $"{x.ProjectRole.GetDisplayName()}: {x.FullName}",
                    Value = x.UserId
                }).ToList();
                p.TeamMembers = teamMembersSelectList;
            }
            return View(projectsResult);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProjectViewModel addProjectViewModel, IFormFile file)
        {
            addProjectViewModel.UserId = _userManager.GetUserId(User);
            var result = await _projectManager.CreateAsync(addProjectViewModel);
            if (result.IsSucceeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "hata var");
            return View(result);
        }
        public async Task<IActionResult> Detail(int projectId)
        {
            var response = await _projectManager.GetByIdAsync(projectId);
            var teamMembersResponse = await _teamMemberManager.GetTeamMembersByProjectIdAsync(projectId);
            var pTaskResponse = await _pTaskManager.GetTasksByProjectIdAsync(projectId);
            var result = _mapperly.ProjectViewModelToEditProjectViewModel(response.Data);
            result.PTasks = pTaskResponse.Data;
            result.TeamMembers = teamMembersResponse.Data;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectViewModel editProjectViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var projectViewModel = _mapperly.EditProjectViewModelToProjectViewModel(editProjectViewModel);
            var createdResponse = await _projectManager.UpdateAsync(projectViewModel);
            if (createdResponse.IsSucceeded)
            {
                return RedirectToAction("Index");
            }
            return View(createdResponse);
        }
        public async Task<IActionResult> Remove(int projectId)
        {
            await _projectManager.SoftDeleteAsync(projectId);
            await _projectManager.ClearAllTasksAsync(projectId);
            await _projectManager.ClearAllTeamMembersAsync(projectId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int projectId, Status status)
        {
            await _projectManager.ChangeProjectStatusAsync(projectId, status);
            string icon = "success";
            string title = $"Durum \"{status.GetDisplayName()}\" olarak güncellendi";
            return Json(new { success = true, icon = icon, title = title });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePriority(int projectId, Priority priority)
        {
            await _projectManager.ChangeProjectPriorityAsync(projectId, priority);
            string icon = "success";
            string title = $"Öncelik \"{priority.GetDisplayName()}\" olarak güncellendi";
            return Json(new { success = true, icon = icon, title = title });
        }
    }
}
