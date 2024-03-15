using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
