using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;
using Yonetimsell.Shared.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Yonetimsell.Business.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yonetimsell.Shared.ComplexTypes;
using Microsoft.CodeAnalysis;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
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
                Friendships = friendList,
                CurrentUserId = userId
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeamMember(string userId, int projectId, ProjectRole projectRole, string currentUserId)
        {
            //Buraya zaten olup olmadığı kontrolü eklenmeli
            var teamMemberViewModel = new TeamMemberViewModel
            {
                UserId = userId,
                ProjectId = projectId,
                ProjectRole = projectRole
            };
            var response = await _teamMemberManager.AddUserToProjectAsync(teamMemberViewModel);
            if (response.IsSucceeded)
            {
                return RedirectToAction("Detail", "Project", new { projectId = projectId });
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı takıma eklenemedi");
                return RedirectToAction("AddTeamMember", new { projectId = projectId });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveTeamMember(int id, string projectId)
        {
            await _teamMemberManager.RemoveUserFromProjectAsync(id);
            return Redirect($"/Customer/Project/Detail?projectId={projectId}");
        }

    }
}
