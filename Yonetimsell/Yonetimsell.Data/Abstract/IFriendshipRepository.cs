using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Abstract
{
    public interface IFriendshipRepository : IGenericRepository<Friendship>
    {
        Task<List<Friendship>> GetFriendListByUserIdAsync(string userId);
        Task<List<Friendship>> GetFriendListByStatusAsync(string userId, FriendshipStatus status);
        Task<bool> CheckIfFriendshipExistsAsync(string currentUserId, string otherUserId);
    }
}
