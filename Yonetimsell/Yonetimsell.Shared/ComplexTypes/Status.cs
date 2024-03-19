using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
