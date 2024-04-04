using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

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

        public ProjectController(UserManager<User> userManager, IProjectService projectManager, ITeamMemberService teamMemberManager, IPTaskService pTaskManager, MapperlyConfiguration mapperly)
        {
            _userManager = userManager;
            _projectManager = projectManager;
            _teamMemberManager = teamMemberManager;
            _pTaskManager = pTaskManager;
            _mapperly = mapperly;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var projects = await _projectManager.GetProjectsByUserIdAsync(userId);
            if (!projects.IsSucceeded)
            {
                return Redirect("/Customer");
            }
            return View(projects.Data);
        }
        public IActionResult Create()
        {
            return View(new AddProjectViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProjectViewModel addProjectViewModel)
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
    }
}
