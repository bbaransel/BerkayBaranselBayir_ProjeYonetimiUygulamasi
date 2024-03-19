using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum ProjectRoles
    {
        [Display(Name = "Proje Yöneticisi")]
        Manager = 0,

        [Display(Name = "Görevli")]
        Employee = 1
    }
}
