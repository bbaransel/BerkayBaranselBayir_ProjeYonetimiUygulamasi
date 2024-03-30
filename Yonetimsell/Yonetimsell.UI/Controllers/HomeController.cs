using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

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
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                return Redirect("/Customer");
            }
            else
            {
                return View();
            }
        }

    }
}
