using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.SubscriptionViewModels
{
    public class SubscribeViewModel
    {
        //User info
        public string UserId { get; set; }
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string FirstName { get; set; }
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string LastName { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string Adress { get; set; }
        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string City { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string Email { get; set; }
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string PhoneNumber { get; set; }
        //Payment info
        [DisplayName("Kart Sahibi")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string CardName { get; set; }
        [DisplayName("Kart Numarası")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string CardNumber { get; set; }
        [DisplayName("Son kullanma Tarihi Ay")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string ExpirationMonth { get; set; }
        [DisplayName("Son kullanma Tarihi Yıl")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string ExpirationYear { get; set; }
        [DisplayName("Cvc")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız")]
        public string Cvc { get; set; }
        [DisplayName("Fiyat")]
        public decimal Price { get; set; }
        [DisplayName("Plan")]
        public SubscriptionPlan SubscriptionPlan { get; set; }

    }
}
