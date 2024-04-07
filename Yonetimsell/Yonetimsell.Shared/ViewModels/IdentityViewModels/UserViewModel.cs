using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string LastName { get; set; }

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Address { get; set; }

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string City { get; set; }

        [DisplayName("Cinsiyet")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Gender { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Email { get; set; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim")]
        public string ImageUrl { get; set; }

        [DisplayName("Doğum Tarihi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public DateTime? DateOfBirth { get; set; }
    }
}
