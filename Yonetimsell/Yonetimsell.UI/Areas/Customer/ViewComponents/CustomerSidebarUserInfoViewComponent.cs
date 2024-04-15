using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.UI.Areas.Customer.Models;

namespace Yonetimsell.UI.Areas.Customer.ViewComponents
{
    public class CustomerSidebarUserInfoViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public CustomerSidebarUserInfoViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var result = new SidebarUserInfoViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                ImageUrl = user.ImageUrl,
            };
            return View(result);
        }
    }
}
