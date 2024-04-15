using Microsoft.EntityFrameworkCore;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task<List<Friendship>> GetFriendListByUserIdAsync(string userId)
        {
            var friendList = await YonetimsellDbContext.Friendship
                .Include(f => f.SenderUser)
                .Include(f => f.ReceiverUser)
                .Where(x => (x.SenderUserId == userId || x.ReceiverUserId == userId) && x.Status == FriendshipStatus.Accepted)
                .ToListAsync();
            return friendList;
        }
        public async Task<List<Friendship>> GetFriendListByStatusAsync(string userId, FriendshipStatus status)
        {
            var friendList = await YonetimsellDbContext.Friendship
                .Include(f => f.SenderUser)
                .Include(f => f.ReceiverUser)
                .Where(x => (x.SenderUserId == userId || x.ReceiverUserId == userId) && x.Status == status)
                .ToListAsync();
            return friendList;
        }
        public async Task<bool> CheckIfFriendshipExistsAsync(string currentUserId, string otherUserId)
        {
            var friendList = await YonetimsellDbContext.Friendship
                .Include(f => f.SenderUser)
                .Include(f => f.ReceiverUser)
                .Where(x => (x.SenderUserId == currentUserId || x.ReceiverUserId == currentUserId) && (x.Status == FriendshipStatus.Accepted || x.Status == FriendshipStatus.Pending))
                .ToListAsync();
            var isExists = friendList.Any(x =>
                (x.SenderUserId == currentUserId && x.ReceiverUserId == otherUserId) || (x.SenderUserId == otherUserId && x.ReceiverUserId == currentUserId));
            return isExists;
        }
    }
}
