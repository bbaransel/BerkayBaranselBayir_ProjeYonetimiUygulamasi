using Microsoft.AspNetCore.Mvc.Rendering;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.UI.Areas.Customer.Models
{
    public class AssignPTaskToTeamMemberViewModel
    {
        public AddPTaskViewModel AddPTaskViewModel { get; set; }
        public List<SelectListItem> TeamMembers { get; set; }
    }
}
