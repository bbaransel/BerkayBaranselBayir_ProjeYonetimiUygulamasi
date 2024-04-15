using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Concrete;
using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class AdminFriendRequestNotificationViewComponent : ViewComponent
    {
        private readonly IFriendshipService _friendshipManager;
        private readonly UserManager<User> _userManager;

        public AdminFriendRequestNotificationViewComponent(IFriendshipService friendshipManager, UserManager<User> userManager)
        {
            _friendshipManager = friendshipManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var response = await _friendshipManager.GetPendingFriendListAsync(userId);
            var result = response.Data.Where(x => x.SenderUserId != userId).ToList();
            return View(result);
        }
    }
}
