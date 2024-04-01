using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class TeamMemberManager : ITeamMemberService
    {
        private readonly ITeamMemberRepository _repository;
        private readonly MapperlyConfiguration _mapperly;

        public TeamMemberManager(ITeamMemberRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;
        }

        public async Task<Response<TeamMemberViewModel>> AddUserToProject(TeamMemberViewModel teamMemberViewModel)
        {
            var teamMember = _mapperly.TeamMemberViewModelToTeamMember(teamMemberViewModel);
            var createdTeamMember = await _repository.CreateAsync(teamMember);
            if (createdTeamMember == null) Response<NoContent>.Fail("Kullanıcı projeye eklenemedi. Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            var result = _mapperly.TeamMemberToTeamMemberViewModel(createdTeamMember);
            return Response<TeamMemberViewModel>.Success(result);
        }

        public async Task<Response<NoContent>> ChangeUsersProjectRole(TeamMemberViewModel teamMemberViewModel)
        {
            var teamMember = await _repository.GetAsync(x=>x.Id == teamMemberViewModel.Id);
            if (teamMember == null) Response<NoContent>.Fail("İlgili takım arkadaşı bulunamadı");
            teamMember.ProjectRole = teamMemberViewModel.ProjectRole;
            await _repository.UpdateAsync(teamMember);
            return Response<NoContent>.Success();
        }

        public async Task<Response<List<TeamMemberViewModel>>> GetTeamMembersByProjectIdAsync(int projectId)
        {
            var teamMembers = await _repository.GetAllAsync(x=>x.ProjectId == projectId);
            if (teamMembers == null) Response<NoContent>.Fail("Hiç takım arkadaşı bulunamadı");
            var result = _mapperly.ListTeamMemberToListTeamMemberViewModel(teamMembers);
            return Response<List<TeamMemberViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> RemoveUserFromProject(TeamMemberViewModel teamMemberViewModel)
        {
            var teamMember = _mapperly.TeamMemberViewModelToTeamMember(teamMemberViewModel);
            if (teamMember == null) Response<NoContent>.Fail("İlgili takım arkadaşı bulunamadı");
            await _repository.HardDeleteAsync(teamMember);
            return Response<NoContent>.Success();
        }
    }
}
