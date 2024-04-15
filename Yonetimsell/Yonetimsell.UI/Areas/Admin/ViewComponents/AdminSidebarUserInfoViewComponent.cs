using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.UI.Areas.Admin.Models;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class AdminSidebarUserInfoViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminSidebarUserInfoViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var result = new AdminUserInfoViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                ImageUrl = user.ImageUrl,
                Role = roles.First(),
            };
            return View(result);
        }
    }
}
