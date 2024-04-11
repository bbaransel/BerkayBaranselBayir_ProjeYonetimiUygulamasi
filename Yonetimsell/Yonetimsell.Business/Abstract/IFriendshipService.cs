using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.FriendshipViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IFriendshipService
    {
        Task<Response<FriendshipViewModel>> GetByIdAsync(int id);
        Task<Response<FriendshipViewModel>> SendFriendRequestAsync(FriendshipViewModel friendshipViewModel);
        Task<Response<List<FriendshipViewModel>>> GetFriendListAsync(string userId);
        Task<Response<List<FriendshipViewModel>>> GetPendingFriendListAsync(string userId);
        Task<Response<NoContent>> RemoveFriendAsync(string userId, string removedUserId);
        Task<Response<NoContent>> DeleteFriendshipByIdAsync(int id);
        Task<Response<bool>> CheckIfFriendshipExistsAsync(string currentUserId, string otherUserId);
        Task<Response<NoContent>> AcceptFriendRequestAsync(int id);
        Task<Response<NoContent>> DenyFriendRequestAsync(int id);
    }
}
