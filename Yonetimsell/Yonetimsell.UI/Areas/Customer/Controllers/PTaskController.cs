using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
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

        public PTaskController(IPTaskService pTaskManager, ITeamMemberService teamMemberManager, UserManager<User> userManager)
        {
            _pTaskManager = pTaskManager;
            _teamMemberManager = teamMemberManager;
            _userManager = userManager;
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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen boş alan bırakmayanız.");
                return RedirectToAction("AddTask",assignPTaskToTeamMemberViewModel.AddPTaskViewModel.ProjectId);
            }
            var createdTask = await _pTaskManager.CreateAsync(assignPTaskToTeamMemberViewModel.AddPTaskViewModel);
            if (createdTask.IsSucceeded)
            {
                return Redirect($"/Customer/Project/Detail?projectId={assignPTaskToTeamMemberViewModel.AddPTaskViewModel.ProjectId}");
            }
            ModelState.AddModelError("", "Görev atanırken bir sorun oluştu.");
            return View(assignPTaskToTeamMemberViewModel.AddPTaskViewModel);
        }

    }
}
