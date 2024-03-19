using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string Username { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
