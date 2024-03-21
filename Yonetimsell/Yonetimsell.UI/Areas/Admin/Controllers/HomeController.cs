using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "SuperAdmin, Admin")]
        [Area("Admin")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
