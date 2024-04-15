using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum FolderName
    {
        [Display(Name = "users")]
        Users = 0,
        [Display(Name = "ptasks")]
        PTasks = 1,
    }
}
