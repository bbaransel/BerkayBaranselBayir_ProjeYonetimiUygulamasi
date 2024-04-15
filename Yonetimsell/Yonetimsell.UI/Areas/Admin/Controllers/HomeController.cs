using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.UI.Areas.Admin.Models;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IProjectService _projectManager;
        private readonly IPTaskService _pTaskManager;
        private readonly ISubscriptionService _subscriptionManager;
        private readonly UserManager<User> _userManager;

        public HomeController(IProjectService projectManager, IPTaskService pTaskManager, ISubscriptionService subscriptionManager, UserManager<User> userManager)
        {
            _projectManager = projectManager;
            _pTaskManager = pTaskManager;
            _subscriptionManager = subscriptionManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var projectCount = await _projectManager.GetAllProjectCountAsync();
            var activeProjectCount = await _projectManager.GetActiveProjectCountAsync();
            var completedProjectCount = await _projectManager.GetCompletedProjectCountAsync();
            var pTaskCount = await _pTaskManager.GetAllTaskCountAsync();
            var activePTaskCount = await _pTaskManager.GetActiveTaskCountAsync();
            var completedPTaskCount = await _pTaskManager.GetCompletedTaskCountAsync();
            var userCount = await _userManager.Users.CountAsync();
            var subscriptionCount = await _subscriptionManager.GetSubscriptionCountAsync();
            var result = new HomePageViewModel
            {
                TotalProjectCount = projectCount.Data,
                ActiveProjectCount = activeProjectCount.Data,
                CompletedProjectCount = completedProjectCount.Data,
                TotalPTaskCount = pTaskCount.Data,
                ActivePTaskCount = activePTaskCount.Data,
                CompletedPTaskCount = completedPTaskCount.Data,
                TotalUserCount = userCount,
                TotalSubscriptionCount= subscriptionCount.Data,
            };
            return View(result);
        }
    }
}
