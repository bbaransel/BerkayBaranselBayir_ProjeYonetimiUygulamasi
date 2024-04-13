using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum FolderName
    {
        [Display(Name ="users")]
        Users = 0,
        [Display(Name = "ptasks")]
        PTasks = 1,
    }
}
