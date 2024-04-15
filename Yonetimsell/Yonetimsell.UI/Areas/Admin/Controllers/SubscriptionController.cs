using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin, Admin")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionManager;

        public SubscriptionController(ISubscriptionService subscriptionManager)
        {
            _subscriptionManager = subscriptionManager;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _subscriptionManager.GetAllAsync();
            return View(response.Data);
        }
    }
}
