using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum SubscriptionPlan
    {
        [Display(Name = "Temel")]
        Basic = 0,
        [Display(Name = "Standart")]
        Standard = 1,
        [Display(Name = "Özel")]
        Premium = 2,
    }
}
