using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.Shared.ViewModels.SubscriptionViewModels
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime SubscriptionDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddDays(30);
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
