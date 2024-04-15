using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class AdminAddUserViewModel
    {
        public string Id { get; set; }
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string FirstName { get; set; }
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string LastName { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Kullanıcı adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string UserName { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Email { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Address { get; set; }
        [DisplayName("Şehir")]
        public string City { get; set; }
        [DisplayName("Görsel Url")]
        public string ImageUrl { get; set; } = "/files/images/default.png";
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string PhoneNumber { get; set; }
        [DisplayName("Cinsiyet")]
        public Gender Gender { get; set; }
        [DisplayName("Doğum Tarihi")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Rolü")]
        public List<AssignRolesViewModel> Roles { get; set; }
    }
}
