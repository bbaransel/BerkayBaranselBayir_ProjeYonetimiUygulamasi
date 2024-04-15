using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.Shared.ViewModels.SubscriptionViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Shared.ViewModels.ProjectViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        [DisplayName("Proje Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        [DisplayName("Proje Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [DisplayName("Bitiş Tarihi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public DateTime EndDate { get; set; }
        [DisplayName("Bütçe")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public decimal Budget { get; set; }
        [DisplayName("Öncelik")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public Priority Priority { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public Status Status { get; set; }
        public bool IsDeleted { get; set; }
        public List<PTaskViewModel> PTasks { get; set; }
        public List<TeamMemberViewModel> TeamMembers { get; set; }
        public List<SubscriptionViewModel> Subscriptions { get; set; }
        public int ProgressPercentage
        {
            get
            {
                var totalDays = (int)Math.Ceiling((EndDate - CreatedDate).TotalDays);
                if (totalDays <= 0) return 100;
                var elapsedDays = (int)Math.Ceiling((DateTime.Now - CreatedDate).TotalDays);
                if (elapsedDays < 0) return 0;
                var progress = (int)Math.Ceiling((elapsedDays * 100.0) / totalDays);
                return progress;
            }
        }
    }
}
