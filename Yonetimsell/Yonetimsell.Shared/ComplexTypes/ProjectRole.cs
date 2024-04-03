using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ComplexTypes
{
    public enum ProjectRole
    {
        [Display(Name = "Proje Yöneticisi")]
        Manager = 0,

        [Display(Name = "Geliştirici")]
        Developer = 1,

        [Display(Name = "Testçi")]
        Tester = 2,

        [Display(Name = "Analist")]
        Analyst = 3,

        [Display(Name = "Tasarımcı")]
        Designer = 4,
    }
}
