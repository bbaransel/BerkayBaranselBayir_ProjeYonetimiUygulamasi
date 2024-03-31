using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IFriendshipService
    {
        Task<Response<FriendshipViewModel>> SendFriendRequestAsync(FriendshipViewModel friendshipViewModel);
        Task<Response<FriendshipViewModel>> ReplyFriendRequestAsync(FriendshipViewModel friendshipViewModel);
        Task<Response<List<FriendshipViewModel>>> GetFriendListAsync(string userId);
        Task<Response<NoContent>> RemoveFriendAsync(string userId, string removedUserId);
    }
}
