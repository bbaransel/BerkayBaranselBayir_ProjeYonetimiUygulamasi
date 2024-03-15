using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yonetimsell.UI.Models;

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
