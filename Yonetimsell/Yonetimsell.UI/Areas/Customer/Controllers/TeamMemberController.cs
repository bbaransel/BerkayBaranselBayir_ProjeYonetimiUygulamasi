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
using Yonetimsell.Shared.Helpers.Abstract;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberManager;
        private readonly IFriendshipService _friendshipManager;
        private readonly UserManager<User> _userManager;
        private readonly ISweetAlertService _sweetAlert;
        private readonly ISubscriptionService _subscriptionManager;

        public TeamMemberController(ITeamMemberService teamMemberManager, IFriendshipService friendshipManager, UserManager<User> userManager, ISweetAlertService sweetAlert)
        {
            _teamMemberManager = teamMemberManager;
            _friendshipManager = friendshipManager;
            _userManager = userManager;
            _sweetAlert = sweetAlert;
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
            var checkResponse = await _teamMemberManager.CheckIfExistsAsync(userId, projectId);
            var SubscriptionResponse = await _subscriptionManager.GetActiveAsync(userId);
            var teamMemberCount = await _teamMemberManager.TeamMemberCountAsync(projectId);
            if(!SubscriptionResponse.IsSucceeded && teamMemberCount.Data == 3)
            {
                TempData["TeamMemberToast"] = _sweetAlert.MiddleNotification("warning", "Mevcut planınıza göre 3ten fazla takım arkadaşı ekleyemezsiniz!");
                return RedirectToAction("AddTeamMember", new { projectId });
            }
            if (checkResponse.Data)
            {
                TempData["TeamMemberToast"] = _sweetAlert.MiddleNotification("error", "Kullanıcı zaten takım arkadaşınız!");
                return RedirectToAction("AddTeamMember", new { projectId });
            }
            var teamMemberViewModel = new TeamMemberViewModel
            {
                UserId = userId,
                ProjectId = projectId,
                ProjectRole = projectRole
            };
            var response = await _teamMemberManager.AddUserToProjectAsync(teamMemberViewModel);
            if (response.IsSucceeded)
            {
                TempData["TeamMemberToast"] = _sweetAlert.MiddleNotification("success", "Kullanıcı takım arkadaşlarına eklendi");
                return RedirectToAction("Detail", "Project", new { projectId });
            }
            else
            {
                TempData["TeamMemberToast"] = _sweetAlert.MiddleNotification("error", "Kullanıcı takım arkadaşlarına eklenemedi!");
                return RedirectToAction("AddTeamMember", new {  projectId });
            }
        }
        public async Task<IActionResult> RemoveTeamMember(int id)
        {
            var response = await _teamMemberManager.GetTeamMemberByIdAsync(id);
            var projectId = response.Data.ProjectId;
            await _teamMemberManager.RemoveUserFromProjectAsync(id);
            return Redirect($"/Customer/Project/Detail?projectId={projectId}");
        }

    }
}
