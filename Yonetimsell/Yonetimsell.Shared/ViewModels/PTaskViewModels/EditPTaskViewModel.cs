using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.PTaskViewModels
{
    public class EditPTaskViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Görev Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Name { get; set; }
        [DisplayName("Görev Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Kullanıcı idsi boş bırakılamaz.")]
        public string UserId { get; set; }
        [DisplayName("Kimin görevi")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [DisplayName("Bitiş tarihi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public DateTime DueDate { get; set; }
        [DisplayName("Öncelik")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public Priority Priority { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public Status Status { get; set; }
        public List<PTaskFileViewModel> PTaskFiles { get; set; }
    }
}
