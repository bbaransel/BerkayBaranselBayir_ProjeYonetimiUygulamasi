using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum Status
    {
        [Display(Name = "Devam ediyor")]
        Continues = 0,

        [Display(Name = "Tamamlandı")]
        Done = 1,

        [Display(Name = "Hatalı")]
        Stuck = 2,

        [Display(Name = "Beklemede")]
        OnHold = 3
    }
}
