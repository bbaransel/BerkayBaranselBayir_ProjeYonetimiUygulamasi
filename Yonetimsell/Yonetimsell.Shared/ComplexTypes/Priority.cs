using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum Priority
    {
        [Display(Name = "Düşük")]
        Low = 0,

        [Display(Name = "Orta")]
        Medium = 1,

        [Display(Name = "Yüksek")]
        High = 2,

        [Display(Name = "Kritik")]
        Critical = 3
    }
}
