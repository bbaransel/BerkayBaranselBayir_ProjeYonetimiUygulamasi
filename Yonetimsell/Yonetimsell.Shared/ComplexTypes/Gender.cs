using System.ComponentModel.DataAnnotations;

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
