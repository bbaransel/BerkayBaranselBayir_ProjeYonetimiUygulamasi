using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum Gender
    {
        [Display(Name = "Kadın")]
        Woman = 0,

        [Display(Name = "Erkek")]
        Man = 1,

        [Display(Name = "Diğer")]
        Other = 2

    }
}
