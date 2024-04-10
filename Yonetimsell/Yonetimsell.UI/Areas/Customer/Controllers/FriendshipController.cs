using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.FriendshipViewModels;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipManager;
        private readonly UserManager<User> _userManager;
        private readonly MapperlyConfiguration _mapperly;

        public FriendshipController(IFriendshipService friendshipManager, UserManager<User> userManager, MapperlyConfiguration mapperly)
        {
            _friendshipManager = friendshipManager;
            _userManager = userManager;
            _mapperly = mapperly;
        }

        public IActionResult Index()
        {
            var currentUserId = _userManager.GetUserId(User);
            var userList = _userManager.Users.Where(x=>x.Id!=currentUserId).ToList();
            var addFriendUserViewModels = new List<AddFriendUserViewModel>();
            foreach (var user in userList)
            {
                var mappedResult = _mapperly.UserToAddFriendUserViewModel(user);
                addFriendUserViewModels.Add(mappedResult);
            }
            return View(addFriendUserViewModels);
        }
        public async Task<IActionResult> FriendList()
        {
            var userId = _userManager.GetUserId(User);
            var response = await _friendshipManager.GetFriendListAsync(userId);
            var friendList = response.Data;
            return View(friendList);
        }
        public async Task<IActionResult> SendFriendRequest(string rUserId)
        {
            var userId = _userManager.GetUserId(User);
            //buraya zaten arkadaş listesinde olup olmadığı kontrolü eklenecek
            var friendship = new FriendshipViewModel { ReceiverUserId = rUserId, SenderUserId = userId, Status = FriendshipStatus.Accepted}; // "Status Accepted" will change later on
            var sentFriendship = await _friendshipManager.SendFriendRequestAsync(friendship);
            return RedirectToAction("Index");
        }

    }
}
