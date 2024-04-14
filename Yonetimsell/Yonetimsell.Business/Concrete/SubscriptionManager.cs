using Microsoft.EntityFrameworkCore;
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
using Yonetimsell.Shared.ViewModels.SubscriptionViewModels;

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

        public async Task<Response<NoContent>> CancelAsync(SubscriptionViewModel subscriptionViewModel)
        {
            var subscription = _mapperly.SubscriptionViewModelToSubscription(subscriptionViewModel);
            if (subscription == null)
            {
                return Response<NoContent>.Fail("Geçerli aboneliğiniz bulunmamaktadır");
            }
            subscription.ExpiryDate = DateTime.Now;
            subscription.IsActive = false;
            await _repository.UpdateAsync(subscription);
            return Response<NoContent>.Success();

        }

        public async Task<Response<SubscriptionStatus>> CheckSubscriptionStatusAsync(int subscriptionId)
        {
            var subscription = await _repository.GetAsync(x => x.Id == subscriptionId);
            if (subscription == null)
            {
                return Response<SubscriptionStatus>.Fail("Üyelik bulunamadı");
            }
            var result = new SubscriptionStatus
            {
                IsActive = subscription.IsActive,
                IsExpired = subscription.ExpiryDate > DateTime.Now ? false : true,
            };
            return Response<SubscriptionStatus>.Success(result);
        }

        public async Task<Response<SubscriptionViewModel>> CreateAsync(SubscriptionViewModel subscriptionViewModel)
        {
            var subscription = _mapperly.SubscriptionViewModelToSubscription(subscriptionViewModel);
            var createdSubscription = await _repository.CreateAsync(subscription);
            if (createdSubscription == null)
            {
                return Response<SubscriptionViewModel>.Fail("Abonelik oluşturulamadı");
            }
            var result = _mapperly.SubscriptionToSubscriptionViewModel(createdSubscription);
            return Response<SubscriptionViewModel>.Success(result);
        }

        public async Task<Response<SubscriptionViewModel>> GetByIdAsync(int subscriptionId)
        {
            var subscription = await _repository.GetAsync(x => x.Id == subscriptionId);
            if (subscription == null)
            {
                return Response<SubscriptionViewModel>.Fail("Üyelik bulunamadı");
            }
            var result = _mapperly.SubscriptionToSubscriptionViewModel(subscription);
            return Response<SubscriptionViewModel>.Success(result);
        }

        public async Task<Response<List<SubscriptionViewModel>>> GetSubscriptionsByUserIdAsync(string userId)
        {
            var subscriptions = await _repository.GetAllAsync(x => x.UserId == userId);
            if (subscriptions == null) 
            { 
                return Response<List<SubscriptionViewModel>>.Fail("Abonelik bilgisi bulunamadı"); 
            }
            var result = _mapperly.ListSubscriptionToListSubscriptionViewModel(subscriptions);
            return Response<List<SubscriptionViewModel>>.Success(result);
        }

        public async Task<Response<SubscriptionViewModel>> RenewSubscriptionAsync(int subscriptionId)
        {
            var oldSubscription = await _repository.GetAsync(x => x.Id == subscriptionId,
                query => query.Include(y => y.User));
            if (oldSubscription == null) 
            {
                return Response<SubscriptionViewModel>.Fail("Üyelik bulunamadı");
            }
            var newSubscription = new Subscription
            {
                SubscriptionPlan = oldSubscription.SubscriptionPlan,
                UserId = oldSubscription.UserId,
                User = oldSubscription.User,
            };
            var renewedSubscription = await _repository.CreateAsync(newSubscription);
            if (renewedSubscription == null) Response<NoContent>.Fail("Abonelik yenilenemedi");
            var result = _mapperly.SubscriptionToSubscriptionViewModel(renewedSubscription);
            return Response<SubscriptionViewModel>.Success(result);
        }

        public async Task<Response<SubscriptionViewModel>> UpdateAsync(SubscriptionViewModel subscriptionViewModel)
        {
            var subscription = _mapperly.SubscriptionViewModelToSubscription(subscriptionViewModel);
            if (subscription == null) 
            {
                return Response<SubscriptionViewModel>.Fail("Geçerli abonelik bulunamadı");
            }
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
        public async Task<Response<SubscriptionViewModel>> GetActiveAsync(string userId)
        {
            var subscription = await _repository.GetActiveSubscriptionByUserIdAsync(userId);
            if (subscription == null)
            {
                return Response<SubscriptionViewModel>.Fail("Aktif üyeliğiniz bulunmamaktadır.");
            }
            var result = _mapperly.SubscriptionToSubscriptionViewModel(subscription);
            return Response<SubscriptionViewModel>.Success(result);
        }
    }
}
