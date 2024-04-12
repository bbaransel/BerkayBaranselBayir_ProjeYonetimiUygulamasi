using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.UI.Areas.Customer.Models;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProjectService _projectManager;
        private readonly IPTaskService _pTaskManager;

        public HomeController(UserManager<User> userManager, IProjectService projectManager, IPTaskService pTaskManager)
        {
            _userManager = userManager;
            _projectManager = projectManager;
            _pTaskManager = pTaskManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var projectCount = await _projectManager.GetActiveProjectCountByUserIdAsync(userId);
            var completedProjectCount = await _projectManager.GetCompletedProjectCountByUserIdAsync(userId);
            var pTaskCount = await _pTaskManager.GetActiveTaskCountByUserIdAsync(userId);
            var completedPTaskCount = await _pTaskManager.GetCompletedTaskCountByUserIdAsync(userId);
            var result = new HomePageViewModel
            {
                ActiveProjectCount = projectCount.Data,
                CompletedProjectCount = completedProjectCount.Data,
                ActivePTaskCount = pTaskCount.Data,
                CompletedPTaskCount = completedPTaskCount.Data
            };
            return View(result);
        }
    }
}
