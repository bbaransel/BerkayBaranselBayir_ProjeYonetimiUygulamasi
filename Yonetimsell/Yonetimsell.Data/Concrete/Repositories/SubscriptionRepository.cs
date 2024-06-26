﻿using Microsoft.EntityFrameworkCore;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task<Subscription> CancelSubscriptionAsync(string userId)
        {
            var subscription = await YonetimsellDbContext.Subscriptions.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            subscription.IsActive = false;
            subscription.ExpiryDate = DateTime.Now;
            YonetimsellDbContext.Subscriptions.Update(subscription);
            return subscription;
        }

        public async Task<Subscription> GetActiveSubscriptionByUserIdAsync(string userId)
        {
            var subscription = await YonetimsellDbContext.Subscriptions.Where(x => x.UserId == userId && x.IsActive == true)
                .OrderByDescending(x => x.ExpiryDate)
                .FirstOrDefaultAsync();
            return subscription;
        }
    }
}
