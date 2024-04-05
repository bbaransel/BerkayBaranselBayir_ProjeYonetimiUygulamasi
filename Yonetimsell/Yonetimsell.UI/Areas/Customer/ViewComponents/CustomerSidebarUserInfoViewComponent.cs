using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.UI.Areas.Customer.Models;

namespace Yonetimsell.UI.Areas.Customer.ViewComponents
{
    public class CustomerSidebarUserInfoViewComponent: ViewComponent
    {
        private readonly UserManager<User> _userManger;

        public CustomerSidebarUserInfoViewComponent(UserManager<User> userManger)
        {
            _userManger = userManger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManger.GetUserId(HttpContext.User);
            var user = await _userManger.FindByIdAsync(userId);
            var result = new SidebarUserInfoViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
            };
            return View(result);
        }
    }
}
