using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Concrete;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectManager;
        private readonly UserManager<User> _userManager;
        private readonly ITeamMemberService _teamMemberManager;
        private readonly IPTaskService _pTaskManager;
        private readonly MapperlyConfiguration _mapperly;

        public ProjectController(IProjectService projectManager, UserManager<User> userManager, ITeamMemberService teamMemberManager, IPTaskService pTaskManager, MapperlyConfiguration mapperly)
        {
            _projectManager = projectManager;
            _userManager = userManager;
            _teamMemberManager = teamMemberManager;
            _pTaskManager = pTaskManager;
            _mapperly = mapperly;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _projectManager.GetAllAsync();
            return View(response.Data);
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
            if (editProjectViewModel.IsDeleted)
            {
                await _projectManager.SoftDeleteAsync(editProjectViewModel.Id);
                await _projectManager.ClearAllTasksAsync(editProjectViewModel.Id);
                await _projectManager.ClearAllTeamMembersAsync(editProjectViewModel.Id);
            }
            var projectViewModel = _mapperly.EditProjectViewModelToProjectViewModel(editProjectViewModel);
            var response = await _projectManager.UpdateAsync(projectViewModel);
            if (response.IsSucceeded)
            {
                return RedirectToAction("Index");
            }
            return View(response);
        }
        public async Task<IActionResult> Delete(int projectId)
        {
            await _projectManager.HardDeleteAsync(projectId);
            return RedirectToAction("Index");
        }
    }
}
