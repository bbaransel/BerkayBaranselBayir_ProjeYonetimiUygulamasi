using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.MessageViewModels;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class AdminHomePageMessageListViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageManager;

        public AdminHomePageMessageListViewComponent(UserManager<User> userManager, IMessageService messageManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var response = await _messageManager.GetAllReceivedMessageAsync(userId);
            List<MessageViewModel> messages;
            if (response.IsSucceeded)
            {
                messages = response.Data.Take(4).ToList();
            }
            else
            {
                messages = response.Data;
            }
            return View(messages);
        }
    }
}
