using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "İki şifre birbiri ile uyuşmuyor.")]
        public string RePassword { get; set; }
        public string Token { get; set; }
    }
}
