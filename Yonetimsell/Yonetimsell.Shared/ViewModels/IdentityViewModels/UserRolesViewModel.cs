using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string FirstName { get; set; }
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string LastName { get; set; }
        [DisplayName("Kullanıcı adı")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string UserName { get; set; }
        [DisplayName("Görsel Url")]
        public string ImageUrl { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string Email { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string Address { get; set; }
        [DisplayName("Şehir")]
        public string City { get; set; }
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [DisplayName("Cinsiyet")]
        public Gender Gender { get; set; }
        [DisplayName("Doğum Tarihi")]
        public DateTime? DateOfBirth { get; set; }
        public List<AssignRolesViewModel> Roles { get; set; }
    }
}
