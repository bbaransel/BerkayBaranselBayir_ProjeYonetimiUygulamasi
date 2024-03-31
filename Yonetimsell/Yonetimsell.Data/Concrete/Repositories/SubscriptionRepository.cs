using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(DbContext dbContext) : base(dbContext)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task<List<Subscription>> GetSubscriptionsByUserIdAsync(string userId)
        {
            var subscriptions = await YonetimsellDbContext.Subscriptions.Where(x=> x.UserId == userId).ToListAsync();
            return subscriptions;
        }
    }
}
