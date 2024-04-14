using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.SubscriptionViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface ISubscriptionService
    {
        Task<Response<SubscriptionViewModel>> CreateAsync(SubscriptionViewModel subscriptionViewModel);
        Task<Response<SubscriptionViewModel>> UpdateAsync(SubscriptionViewModel subscriptionViewModel);
        Task<Response<NoContent>> CancelAsync(SubscriptionViewModel subscriptionViewModel);
        Task<Response<SubscriptionViewModel>> GetByIdAsync(int subscriptionId);
        Task<Response<List<SubscriptionViewModel>>> GetSubscriptionsByUserIdAsync(string userId);
        Task<Response<SubscriptionStatus>> CheckSubscriptionStatusAsync(int subscriptionId);
        Task<Response<SubscriptionViewModel>> RenewSubscriptionAsync(int subscriptionId);
        Task<Response<SubscriptionViewModel>> GetActiveAsync(string userId);
    }
}
