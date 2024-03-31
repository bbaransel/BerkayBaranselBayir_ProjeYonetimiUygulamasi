using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.Shared.ViewModels.ProjectViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public bool IsCompleted { get; set; } = false;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<PTaskViewModel> PTasks { get; set; }
        public List<TeamMemberViewModel> TeamMembers { get; set; }
        public List<SubscriptionViewModel> Subscriptions { get; set; }
        public ProgressTime ProgressTime { get; set; }
        //public ProgressTime ProgressTime()
        //{
        //    var designatedDays = (int)Math.Ceiling((EndDate - CreatedDate ).TotalDays);
        //    var passingDays = (int)Math.Ceiling((DateTime.Now - CreatedDate).TotalDays);
        //    var remainingDays = (int)Math.Ceiling((EndDate - DateTime.Now).TotalDays);
        //    var progressedDaysPercentage = (passingDays / designatedDays) * 100;
        //    var result = new ProgressTime
        //    {
        //        DesignatedDays = designatedDays,
        //        PassingDays = passingDays,
        //        RemainingDays = remainingDays,
        //        ProgressedDaysPercentage = progressedDaysPercentage
        //    };
        //    return result;
        //}
    }
}
