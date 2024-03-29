using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.Shared.ViewModels
{
    public class AddProjectViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<PTaskViewModel> PTasks { get; set; }
        public List<TeamMemberViewModel> TeamMembers { get; set; }
        public List<SubscriptionViewModel> Subscriptions { get; set; }
    }
}
