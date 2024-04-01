using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Shared.ViewModels.ProjectViewModels
{
    public class EditProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<PTaskViewModel> PTasks { get; set; }
        public List<TeamMemberViewModel> TeamMembers { get; set; }
        public List<SubscriptionViewModel> Subscriptions { get; set; }
    }
}
