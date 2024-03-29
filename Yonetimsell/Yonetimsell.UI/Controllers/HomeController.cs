using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProjectService _projectManager;

        public HomeController(UserManager<User> userManager, IProjectService projectManager)
        {
            _userManager = userManager;
            _projectManager = projectManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateProject()
        {
            return View( new AddProjectViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(AddProjectViewModel addProjectViewModel)
        {
            addProjectViewModel.UserId = _userManager.GetUserId(User);
            //addProjectViewModel.PTasks = new List<PTaskViewModel>();
            //addProjectViewModel.TeamMembers = new List<TeamMemberViewModel>();
            //addProjectViewModel.Subscriptions = new List<SubscriptionViewModel>();
            addProjectViewModel.EndDate = DateTime.Now;
            var result = await _projectManager.CreateAsync(addProjectViewModel);
            if (result.IsSucceeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "hata var");
            return View(result);
        }
    }
}
