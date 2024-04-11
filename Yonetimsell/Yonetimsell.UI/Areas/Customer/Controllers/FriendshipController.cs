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
using Yonetimsell.Shared.Helpers.Abstract;
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
        private readonly ISweetAlertService _sweetAlert;

        public FriendshipController(IFriendshipService friendshipManager, UserManager<User> userManager, MapperlyConfiguration mapperly, ISweetAlertService sweetAlert)
        {
            _friendshipManager = friendshipManager;
            _userManager = userManager;
            _mapperly = mapperly;
            _sweetAlert = sweetAlert;
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
            var ifFriendshipExists = await _friendshipManager.CheckIfFriendshipExistsAsync(userId, rUserId);
            if(ifFriendshipExists.Data)
            {
                TempData["FriendRequestToast"] = _sweetAlert.MiddleNotification("error", "Arkadaşlık isteği zaten gönderildi");
                return RedirectToAction("Index");
            }
            var friendship = new FriendshipViewModel { ReceiverUserId = rUserId, SenderUserId = userId, Status = FriendshipStatus.Pending};
            var sentFriendship = await _friendshipManager.SendFriendRequestAsync(friendship);
            TempData["FriendRequestToast"] = _sweetAlert.MiddleNotification("success", "Arkadaşlık isteği gönderildi.");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AcceptFriendRequest(int id)
        {
            var response = await _friendshipManager.AcceptFriendRequestAsync(id);
            if (!response.IsSucceeded)
            {
                TempData["RepyRequestToast"] = _sweetAlert.TopEndNotification("error", "İstek bulunamadı.");
                return RedirectToAction("PendingList");
            }
            TempData["RepyRequestToast"] = _sweetAlert.TopEndNotification("success", "Arkadaş başarıyla eklendi.");
            return RedirectToAction("PendingList");
        }
        public async Task<IActionResult> DenyFriendRequest(int id)
        {
            var response = await _friendshipManager.DenyFriendRequestAsync(id);
            if (!response.IsSucceeded)
            {
                TempData["RepyRequestToast"] = _sweetAlert.TopEndNotification("error", "İstek bulunamadı.");
                return RedirectToAction("PendingList");
            }
            TempData["RepyRequestToast"] = _sweetAlert.TopEndNotification("warning", "Arkadaşlık isteği reddedildi.");
            return RedirectToAction("PendingList");
        }
        public async Task<IActionResult> PendingList()
        {
            var userId = _userManager.GetUserId(User);
            var response = await _friendshipManager.GetPendingFriendListAsync(userId);
            var result = response.Data.Where(x => x.SenderUserId != userId).ToList();
            return View(result);
        }

    }
}
