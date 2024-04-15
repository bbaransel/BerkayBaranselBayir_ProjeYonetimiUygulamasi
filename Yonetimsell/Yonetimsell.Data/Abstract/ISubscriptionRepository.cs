using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Abstract
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<Subscription> GetActiveSubscriptionByUserIdAsync(string userId);
        Task<Subscription> CancelSubscriptionAsync(string userId);
    }
}
