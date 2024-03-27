using Microsoft.AspNetCore.Mvc;

namespace Yonetimsell.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
