using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Ad")]
        [Required(ErrorMessage = "Lütfen ad alanını boş bırakmayınız.")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Lütfen soyad alanını boş bırakmayınız.")]
        public string LastName { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı adı alanını boş bırakmayınız.")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Lütfen email alanını boş bırakmayınız.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "Lütfen parola alanını boş bırakmayınız.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Parola Tekrar")]
        [Required(ErrorMessage = "Lütfen parola tekrar alanını boş bırakmayınız.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor, kontrol ediniz.")]
        public string RePassword { get; set; }
    }
}
