using Microsoft.AspNetCore.Mvc.Rendering;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.UI.Areas.Customer.Models
{
    public class AssignPTaskToTeamMemberViewModel
    {
        public AddPTaskViewModel AddPTaskViewModel { get; set; }
        public List<SelectListItem> TeamMembers { get; set; }
    }
}
