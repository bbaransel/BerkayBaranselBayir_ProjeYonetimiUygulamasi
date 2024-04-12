using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface ITeamMemberService
    {
        Task<Response<TeamMemberViewModel>> AddUserToProjectAsync(TeamMemberViewModel teamMemberViewModel);
        Task<Response<NoContent>> RemoveUserFromProjectAsync(int id);
        Task<Response<NoContent>> ChangeUsersProjectRoleAsync(TeamMemberViewModel teamMemberViewModel);
        Task<Response<TeamMemberViewModel>> GetTeamMemberByIdAsync(int id);
        Task<Response<List<TeamMemberViewModel>>> GetTeamMembersByProjectIdAsync(int projectId);
        Task<Response<bool>> CheckIfExistsAsync(string userId, int projectId);
    }
}
