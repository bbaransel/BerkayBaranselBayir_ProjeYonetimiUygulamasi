using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.UI.Areas.Customer.ViewComponents
{
    public class FriendRequestNotificationViewComponent : ViewComponent
    {
        private readonly IFriendshipService _friendshipManager;
        private readonly UserManager<User> _userManager;

        public FriendRequestNotificationViewComponent(IFriendshipService friendshipManager, UserManager<User> userManager)
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
