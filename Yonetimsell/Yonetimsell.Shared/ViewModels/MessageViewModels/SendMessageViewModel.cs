using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ViewModels.MessageViewModels
{
    public class SendMessageViewModel
    {
        public MessageViewModel MessageViewModel { get; set; }
        public List<SelectListItem> FriendList { get; set; }
    }
}
