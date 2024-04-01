using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;
using Yonetimsell.Shared.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Yonetimsell.Business.Mappings;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberManager;
        private readonly IFriendshipService _friendshipManager;
        private readonly UserManager<User> _userManager;

        public TeamMemberController(ITeamMemberService teamMemberManager, IFriendshipService friendshipManager, UserManager<User> userManager)
        {
            _teamMemberManager = teamMemberManager;
            _friendshipManager = friendshipManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> AddTeamMember(int projectId)
        {
            var userId = _userManager.GetUserId(User);
            var response = await _friendshipManager.GetFriendListAsync(userId);
            var friendList = response.Data;
            var teamMemberViewModel = new TeamMemberViewModel();
            teamMemberViewModel.ProjectId = projectId;
            teamMemberViewModel.UserId = userId;
            var result = new AddTeamMemberViewModel
            {
                TeamMemberViewModel = teamMemberViewModel,
                Friendships = friendList
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeamMember(AddTeamMemberViewModel addTeamMemberViewModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Bilgileri kontrol ediniz.");
                return View(addTeamMemberViewModel);
            }
            var response = await _teamMemberManager.AddUserToProject(addTeamMemberViewModel.TeamMemberViewModel);
            if (!response.IsSucceeded)
            {
                ModelState.AddModelError("", "Kullanıcı takıma eklenemedi");
                return View(addTeamMemberViewModel);
            }
            return Redirect($"/Customer/Project/Detail?projectId={addTeamMemberViewModel.TeamMemberViewModel.ProjectId}");
        }
    }
}
