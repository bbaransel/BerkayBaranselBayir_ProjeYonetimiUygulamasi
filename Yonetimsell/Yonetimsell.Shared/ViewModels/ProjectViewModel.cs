using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.Shared.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public bool IsCompleted { get; set; } = false;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<PTaskViewModel> PTasks { get; set; }
        public List<TeamMemberViewModel> TeamMembers { get; set; }
        public List<SubscriptionViewModel> Subscriptions { get; set; }
    }
}
