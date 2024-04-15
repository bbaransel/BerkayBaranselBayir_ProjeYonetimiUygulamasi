using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yonetimsell.Shared.ViewModels.MessageViewModels
{
    public class SendMessageViewModel
    {
        public MessageViewModel MessageViewModel { get; set; }
        public List<SelectListItem> FriendList { get; set; }
    }
}
