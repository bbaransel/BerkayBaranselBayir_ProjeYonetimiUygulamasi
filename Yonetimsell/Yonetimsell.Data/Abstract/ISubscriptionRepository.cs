using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ViewModels.SubscriptionViewModels;

namespace Yonetimsell.Data.Abstract
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<Subscription> GetActiveSubscriptionByUserIdAsync(string userId);
        Task<Subscription> CancelSubscriptionAsync(string userId);
    }
}
