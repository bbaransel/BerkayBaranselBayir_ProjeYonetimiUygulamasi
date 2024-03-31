using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Friendship>> GetFriendListByUserId(string userId)
        {
            var friendList = await YonetimsellDbContext.Friendship.Where(x=>(x.SenderUserId == userId || x.ReceiverUserId==userId)&& x.Status == FriendshipStatus.Accepted).ToListAsync();
            return friendList;
        }
    }
}
