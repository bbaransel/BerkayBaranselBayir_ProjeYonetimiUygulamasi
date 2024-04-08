using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [DisplayName("Ad *")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string FirstName { get; set; }

        [DisplayName("Soyad *")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string LastName { get; set; }

        [DisplayName("Adres")]
        public string Address { get; set; }

        [DisplayName("Şehir")]
        public string City { get; set; }

        [DisplayName("Cinsiyet *")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public Gender Gender { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Email { get; set; }

        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim")]
        public string ImageUrl { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime? DateOfBirth { get; set; }
    }
}
