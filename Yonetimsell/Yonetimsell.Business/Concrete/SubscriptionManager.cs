using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class SubscriptionManager : ISubscriptionService
    {
        public Task<Response<NoContent>> Cancel(SubscriptionViewModel subscriptionViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Response<SubscriptionViewModel>> Create(SubscriptionViewModel subscriptionViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<SubscriptionViewModel>>> GetSubscriptionsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<SubscriptionViewModel>> Update(SubscriptionViewModel subscriptionViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
