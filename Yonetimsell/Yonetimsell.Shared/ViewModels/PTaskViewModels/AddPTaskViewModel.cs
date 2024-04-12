using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

namespace Yonetimsell.Shared.ViewModels.PTaskViewModels
{
    public class AddPTaskViewModel
    {
        public int Id { get; set; }
        [DisplayName("Görev İsmi")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string Name { get; set; }
        [DisplayName("Görev Açıklaması")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Lütfen bir takım arkadaşı seçiniz.")]
        public string UserId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [DisplayName("Bitiş Tarihi")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        [DisplayName("Öncelik")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public Priority Priority { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "{0} alanını boş bırakmayınız.")]
        public Status Status { get; set; }
    }
}
