using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipManager;
        private readonly UserManager<User> _userManager;

        public FriendshipController(IFriendshipService friendshipManager, UserManager<User> userManager)
        {
            _friendshipManager = friendshipManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendFriendRequest(string receiverUserId)
        {
            var userId = _userManager.GetUserId(User);
            var friendship = new FriendshipViewModel { ReceiverUserId = receiverUserId, SenderUserId = userId, Status = FriendshipStatus.Accepted}; // "Status Accepted" will change later on
            var sentFriendship = await _friendshipManager.SendFriendRequestAsync(friendship);
            return Ok(sentFriendship);
        }
    }
}
