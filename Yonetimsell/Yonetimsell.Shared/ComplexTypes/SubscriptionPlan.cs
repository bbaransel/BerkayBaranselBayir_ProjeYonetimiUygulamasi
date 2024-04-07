using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum SubscriptionPlan
    {
        [Display(Name ="Temel")]
        Basic = 0,
        [Display(Name ="Standart")]
        Standard = 1,
        [Display(Name = "Özel")]
        Premium = 2,
    }
}
