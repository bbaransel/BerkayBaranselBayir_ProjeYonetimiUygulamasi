using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.UI.Areas.Admin.Models;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class AdminNavbarUserInfoViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminNavbarUserInfoViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var result = new AdminUserInfoViewModel
            {
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                ImageUrl = user.ImageUrl,
            };
            return View(result);
        }
    }
}
