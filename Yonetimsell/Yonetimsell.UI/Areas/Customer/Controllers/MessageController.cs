using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.ViewModels.MessageViewModels;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageManager;
        private readonly IFriendshipService _friendshipManager;
        private readonly ISweetAlertService _sweetAlert;

        public MessageController(UserManager<User> userManager, IMessageService messageManager, IFriendshipService friendshipManager, ISweetAlertService sweetAlert)
        {
            _userManager = userManager;
            _messageManager = messageManager;
            _friendshipManager = friendshipManager;
            _sweetAlert = sweetAlert;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var messageListResponse =await _messageManager.GetAllReceivedMessageAsync(userId);
            var result = messageListResponse.Data;
            return View(result);
        }
        public async Task<IActionResult> Detail(int messageId)
        {
            var result = await _messageManager.GetByIdAsync(messageId);
            var message = result.Data;
            await _messageManager.ChangeIsRead(messageId);
            if (message.RelatedId != 0)
            {
                var response = await _messageManager.GetAllRelatedMessagesAsync(message.RelatedId);
                var relatedMessages = response.Data;
                return View(relatedMessages);
            }
            var relatedMessageList = new List<MessageViewModel>();
            relatedMessageList.Add(message);
            return View(relatedMessageList);
        }
        public async Task<IActionResult> SendMessage(string reciverId = null)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (reciverId == null)
            {
                var friendsResponse = await _friendshipManager.GetFriendListAsync(userId);
                var friends = friendsResponse.Data;
                List<SelectListItem> friendsSelectList = friends.Select(x => new SelectListItem
                {
                    Text = x.SenderUserName == user.UserName ? x.ReceiverUserName : x.SenderUserName,
                    Value = x.SenderUserId == user.Id ? x.ReceiverUserId : x.SenderUserId,
                }).ToList();
                SendMessageViewModel model = new SendMessageViewModel
                {
                    FriendList = friendsSelectList
                };
                return View(model);
            }
            var reciverUser = await _userManager.FindByIdAsync(reciverId);
            var selectListItem = new SelectListItem
            {
                Text = reciverUser.UserName,
                Value = reciverUser.Id,
            };
            var friendSelectList = new List<SelectListItem>
            {
                selectListItem
            };
            SendMessageViewModel result = new SendMessageViewModel
            {
                FriendList = friendSelectList
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel model)
        {
            var reciverUser = await _userManager.FindByIdAsync(model.MessageViewModel.ReceiverId);
            if (reciverUser == null) 
            {
                TempData["MessageToast"] = _sweetAlert.MiddleNotification("error", "Lütfen kime gönderileceğini seçiniz.");
                return RedirectToAction("SendMessage");
            }
            model.MessageViewModel.ReceiverId = reciverUser.Id;
            model.MessageViewModel.ReceiverFullName = $"{reciverUser.FirstName} {reciverUser.LastName}";
            model.MessageViewModel.ReceiverImageUrl = reciverUser.ImageUrl;
            model.MessageViewModel.ReceiverUserName = reciverUser.UserName;
            var senderUser = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            model.MessageViewModel.SenderId = senderUser.Id;
            model.MessageViewModel.SenderFullName = $"{senderUser.FirstName} {senderUser.LastName}";
            model.MessageViewModel.SenderImageUrl = senderUser.ImageUrl;
            model.MessageViewModel.SenderUserName = senderUser.UserName;
            var result = await _messageManager.CreateAsync(model.MessageViewModel);
            if (result.IsSucceeded)
            {
                TempData["MessageToast"] = _sweetAlert.MiddleNotification("success", "Mesajınız başarıyla gönderildi.");
            }
            else
            {
                TempData["MessageToast"] = _sweetAlert.MiddleNotification("error", "Mesaj gönderilemedi.");
            }
            return RedirectToAction("Index");
        }
    }
}
