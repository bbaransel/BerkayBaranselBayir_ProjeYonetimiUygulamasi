using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
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

        public PTaskController(IPTaskService pTaskManager, ITeamMemberService teamMemberManager)
        {
            _pTaskManager = pTaskManager;
            _teamMemberManager = teamMemberManager;
        }

        public IActionResult Index()
        {
            return View();
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
