using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface ISubscriptionService
    {
        Task<Response<SubscriptionViewModel>> Create(SubscriptionViewModel subscriptionViewModel);
        Task<Response<SubscriptionViewModel>> Update(SubscriptionViewModel subscriptionViewModel);
        Task<Response<NoContent>> Cancel(SubscriptionViewModel subscriptionViewModel);
        Task<Response<List<SubscriptionViewModel>>> GetSubscriptionsByUserIdAsync(string userId);
    }
}
