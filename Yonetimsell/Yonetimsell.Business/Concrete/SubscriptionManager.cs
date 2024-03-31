using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class SubscriptionManager : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;
        private readonly MapperlyConfiguration _mapperly;

        public SubscriptionManager(ISubscriptionRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;
        }

        public async Task<Response<NoContent>> Cancel(SubscriptionViewModel subscriptionViewModel)
        {
            var subscription = _mapperly.SubscriptionViewModelToSubscription(subscriptionViewModel);
            if (subscription == null) Response<NoContent>.Fail("Geçerli aboneliğiniz bulunmamaktadır");
            subscription.ExpiryDate = DateTime.Now;
            subscription.IsActive = false;
            await _repository.UpdateAsync(subscription);
            return Response<NoContent>.Success();

        }

        public async Task<Response<SubscriptionViewModel>> Create(SubscriptionViewModel subscriptionViewModel)
        {
            var subscription = _mapperly.SubscriptionViewModelToSubscription(subscriptionViewModel);
            var createdSubscription = await _repository.CreateAsync(subscription);
            if (createdSubscription == null) Response<NoContent>.Fail("Abonelik oluşturulamadı");
            var result = _mapperly.SubscriptionToSubscriptionViewModel(createdSubscription);
            return Response<SubscriptionViewModel>.Success(result);
        }

        public async Task<Response<List<SubscriptionViewModel>>> GetSubscriptionsByUserIdAsync(string userId)
        {
            var subscriptions = await _repository.GetAllAsync(x => x.UserId == userId);
            if (subscriptions == null) Response<NoContent>.Fail("Abonelik bilgisi bulunamadı");
            var result = _mapperly.ListSubscriptionToListSubscriptionViewModel(subscriptions);
            return Response<List<SubscriptionViewModel>>.Success(result);
        }

        public async Task<Response<SubscriptionViewModel>> Update(SubscriptionViewModel subscriptionViewModel)
        {
            var subscription = _mapperly.SubscriptionViewModelToSubscription(subscriptionViewModel);
            if (subscription == null) Response<NoContent>.Fail("Geçerli abonelik bulunamadı");
            subscription.ExpiryDate = DateTime.UtcNow;
            subscription.IsActive = false;
            var newSubscription = new Subscription
            {
                UserId = subscriptionViewModel.UserId,
                ExpiryDate = subscriptionViewModel.ExpiryDate,
                SubscriptionDate = subscriptionViewModel.ExpiryDate,
                SubscriptionPlan = subscriptionViewModel.SubscriptionPlan,
                IsActive = true
            };
            var createdSubscription = await _repository.CreateAsync(newSubscription);
            if (createdSubscription == null) Response<NoContent>.Fail("Abonelik güncellenemedi");
            var result = _mapperly.SubscriptionToSubscriptionViewModel(createdSubscription);
            return Response<SubscriptionViewModel>.Success(result);
        }
    }
}
