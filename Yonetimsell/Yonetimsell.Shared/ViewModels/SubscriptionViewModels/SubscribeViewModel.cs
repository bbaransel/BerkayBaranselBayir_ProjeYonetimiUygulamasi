using System;
using System.Collections.Generic;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //Payment info
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvc { get; set; }
        public decimal Price { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }

    }
}
