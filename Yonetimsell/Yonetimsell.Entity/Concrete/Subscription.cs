﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class Subscription
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
