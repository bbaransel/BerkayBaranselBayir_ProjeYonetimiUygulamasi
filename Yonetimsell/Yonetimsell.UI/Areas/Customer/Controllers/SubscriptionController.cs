using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.ViewModels.SubscriptionViewModels;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class SubscriptionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ISubscriptionService _subscriptionManager;
        private readonly ISweetAlertService _sweetAlert;

        public SubscriptionController(UserManager<User> userManager, ISubscriptionService subscriptionManager, ISweetAlertService sweetAlert)
        {
            _userManager = userManager;
            _subscriptionManager = subscriptionManager;
            _sweetAlert = sweetAlert;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var response = await _subscriptionManager.GetActiveAsync(userId);
            if (response.IsSucceeded)
            {
                return View(response.Data);
            }
            return View();
        }
        public async Task<IActionResult> Subscribe(SubscriptionPlan subscriptionPlan, decimal price)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var subscribeViewModel = new SubscribeViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Adress = user.Address,
                City = user.City,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CardNumber = "4987490000000002",
                CardName = "LeBron James",
                ExpirationMonth = "8",
                ExpirationYear = "2028",
                Cvc = "343",
                UserId = userId,
                SubscriptionPlan = subscriptionPlan,
                Price = price,
            };
            return View(subscribeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeViewModel subscribeViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var oldSubscriptionResponse = await _subscriptionManager.GetActiveAsync(userId);
            if (oldSubscriptionResponse.IsSucceeded)
            {
                TempData["SubscriptionToast"] = _sweetAlert.MiddleNotification("warning", "Zaten mevcut bir üyeliğiniz bulunmakta!");
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                //ÖDEME İŞLEMİ -- IYZICO

                //Yapılacak ödeme isteiğinin authorization seçenekleri için nesne yaratılıyor.
                Options options = new Options();
                options.ApiKey = "sandbox-lDDqO59vEW5BOSTxLmLExqIwPHa5wEDW";
                options.SecretKey = "sandbox-AcscekvYlBG1fBemVMXzXDzejwYAgLvS";
                options.BaseUrl = "https://sandbox-api.iyzipay.com";
                //Yapılacak ödeme isteği için nesne yaratılıyor.
                CreatePaymentRequest request = new CreatePaymentRequest();
                request.Locale = Locale.TR.ToString();
                request.ConversationId = "Yonetimsell";
                request.Price = subscribeViewModel.Price.ToString().Replace(",", ".");
                request.PaidPrice = subscribeViewModel.Price.ToString().Replace(",", ".");
                request.Currency = Currency.TRY.ToString();
                request.Installment = 1;
                request.BasketId = userId.ToString();
                request.PaymentChannel = PaymentChannel.WEB.ToString();
                request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
                //Ödemenin yapılacağı kart için nesne yaratılıyor.
                PaymentCard paymentCard = new PaymentCard();
                paymentCard.CardHolderName = subscribeViewModel.CardName;
                paymentCard.CardNumber = subscribeViewModel.CardNumber;
                paymentCard.ExpireMonth = subscribeViewModel.ExpirationMonth;
                paymentCard.ExpireYear = subscribeViewModel.ExpirationYear;
                paymentCard.Cvc = subscribeViewModel.Cvc;
                paymentCard.RegisterCard = 0;
                request.PaymentCard = paymentCard;
                //Alıcı bilgileri için nesne yaratılıyor.
                Buyer buyer = new Buyer();
                buyer.Id = userId;
                buyer.Name = subscribeViewModel.FirstName;
                buyer.Surname = subscribeViewModel.LastName;
                buyer.GsmNumber = subscribeViewModel.PhoneNumber;
                buyer.Email = subscribeViewModel.Email;
                buyer.IdentityNumber = "74300864791";
                buyer.LastLoginDate = "2015-10-05 12:43:35";
                buyer.RegistrationDate = "2013-04-21 15:12:09";
                buyer.RegistrationAddress = subscribeViewModel.Adress;
                buyer.Ip = "85.34.78.112";
                buyer.City = subscribeViewModel.City;
                buyer.Country = "Türkiye";
                buyer.ZipCode = "34732";
                request.Buyer = buyer;
                //Adres bilgileri için nesneler yaratılıyor.
                Address shippingAddress = new Address();
                shippingAddress.ContactName = "Jane Doe";
                shippingAddress.City = "Istanbul";
                shippingAddress.Country = "Turkey";
                shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                shippingAddress.ZipCode = "34742";
                request.ShippingAddress = shippingAddress;
                //Fatura Adresi bilgileri için nesneler yaratılıyor.
                Address billingAddress = new Address();
                billingAddress.ContactName = "Jane Doe";
                billingAddress.City = "Istanbul";
                billingAddress.Country = "Turkey";
                billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                billingAddress.ZipCode = "34742";
                request.BillingAddress = billingAddress;
                // Sepet ürünleri için nesneler yaratılıyor.
                List<BasketItem> basketItems = new List<BasketItem>();
                BasketItem basketItem;
                basketItem = new BasketItem();
                basketItem.Id = subscribeViewModel.UserId;
                basketItem.Name = subscribeViewModel.SubscriptionPlan.GetDisplayName();
                basketItem.Category1 = "Üyelik";
                basketItem.Category2 = "Aylık";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = subscribeViewModel.Price.ToString().Replace(",", ".");
                basketItems.Add(basketItem);
                request.BasketItems = basketItems;
                Payment payment = Payment.Create(request, options);
                //Paymentın durumuna göre yapılacak işlemler
                if (payment.Status == "success")
                {
                    //Eğer ödeme başarılı ise siparişi kendi veritabanımıza kayıt ediyoruz.
                    var subscriptionViewModel = new SubscriptionViewModel
                    {
                        UserId = subscribeViewModel.UserId,
                        SubscriptionPlan = subscribeViewModel.SubscriptionPlan,
                    };
                    var createdSubscription = await _subscriptionManager.CreateAsync(subscriptionViewModel);
                    if (createdSubscription.IsSucceeded)
                    {
                        TempData["SubscriptionToast"] = _sweetAlert.MiddleNotification("success", "Üyeliğiniz başarıyla oluşturuldu");
                        return Redirect("/Customer");
                    }
                }
                TempData["SubscriptionToast"] = _sweetAlert.MiddleNotification("error", "Üyelik oluşturulamadı");
                ModelState.AddModelError("", payment.ErrorMessage);
            }
            return View(subscribeViewModel);
        }
    }
}
