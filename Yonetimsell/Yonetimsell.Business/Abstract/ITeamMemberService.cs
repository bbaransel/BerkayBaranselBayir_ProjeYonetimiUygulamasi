using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface ITeamMemberService
    {
        Task<Response<TeamMemberViewModel>> AddUserToProject(TeamMemberViewModel teamMemberViewModel);
        Task<Response<NoContent>> RemoveUserFromProject(TeamMemberViewModel teamMemberViewModel);
        Task<Response<NoContent>> ChangeUsersProjectRole(TeamMemberViewModel teamMemberViewModel);

        Task<Response<List<TeamMemberViewModel>>> GetTeamMembersByProjectIdAsync(int projectId);
    }
}
