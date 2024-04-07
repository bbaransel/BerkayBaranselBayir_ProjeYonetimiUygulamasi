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

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
        public IActionResult About()
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
        public IActionResult Subscriptions()
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
