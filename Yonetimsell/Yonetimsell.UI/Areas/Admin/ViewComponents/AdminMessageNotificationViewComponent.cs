using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.MessageViewModels;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class AdminMessageNotificationViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageManager;

        public AdminMessageNotificationViewComponent(UserManager<User> userManager, IMessageService messageManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var response = await _messageManager.GetAllReceivedMessageAsync(userId, false);
            List<MessageViewModel> messages;
            if (response.IsSucceeded)
            {
                messages = response.Data.Take(3).ToList();
            }
            else
            {
                messages = response.Data;
            }
            return View(messages);
        }
    }
}
