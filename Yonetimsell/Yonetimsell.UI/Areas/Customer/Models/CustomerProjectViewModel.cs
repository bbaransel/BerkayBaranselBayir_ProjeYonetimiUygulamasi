using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yonetimsell.Shared.ViewModels.SubscriptionViewModels;

namespace Yonetimsell.UI.Areas.Customer.Models
{
    public class CustomerProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<PTaskViewModel> PTasks { get; set; }
        public List<SelectListItem> TeamMembers { get; set; }
        public List<SubscriptionViewModel> Subscriptions { get; set; }
        public int ProgressTimePercentage
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
        public int ProgressPTaskPercentage { get; set; }
    }
}
