using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class TeamMemberManager : ITeamMemberService
    {
        private readonly ITeamMemberRepository _repository;
        private readonly MapperlyConfiguration _mapperly;
        private readonly UserManager<User> _userManager;

        public TeamMemberManager(ITeamMemberRepository repository, MapperlyConfiguration mapperly, UserManager<User> userManager)
        {
            _repository = repository;
            _mapperly = mapperly;
            _userManager = userManager;
        }

        public async Task<Response<TeamMemberViewModel>> AddUserToProjectAsync(TeamMemberViewModel teamMemberViewModel)
        {
            var teamMember = _mapperly.TeamMemberViewModelToTeamMember(teamMemberViewModel);
            var createdTeamMember = await _repository.CreateAsync(teamMember);
            if (createdTeamMember == null) Response<NoContent>.Fail("Kullanıcı projeye eklenemedi. Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            var result = _mapperly.TeamMemberToTeamMemberViewModel(createdTeamMember);
            return Response<TeamMemberViewModel>.Success(result);
        }

        public async Task<Response<NoContent>> ChangeUsersProjectRoleAsync(TeamMemberViewModel teamMemberViewModel)
        {
            var teamMember = await _repository.GetAsync(x=>x.Id == teamMemberViewModel.Id);
            if (teamMember == null) Response<NoContent>.Fail("İlgili takım arkadaşı bulunamadı");
            teamMember.ProjectRole = teamMemberViewModel.ProjectRole;
            await _repository.UpdateAsync(teamMember);
            return Response<NoContent>.Success();
        }

        public async Task<Response<List<TeamMemberViewModel>>> GetTeamMembersByProjectIdAsync(int projectId)
        {
            var teamMembers = await _repository.GetAllAsync(x => 
                x.ProjectId == projectId,
                query => query.Include(x => x.User));
            if (teamMembers == null) Response<NoContent>.Fail("Hiç takım arkadaşı bulunamadı");
            var result = teamMembers.Select(x => new TeamMemberViewModel
            {
                Id = x.Id,
                ProjectRole = x.ProjectRole,
                ProjectId = projectId,
                UserId = x.UserId,
                UserName = x.User.UserName,
                FullName = $"{x.User.FirstName} {x.User.LastName}",

            }).ToList();
            return Response<List<TeamMemberViewModel>>.Success(result);
        }
        public async Task<Response<NoContent>> RemoveUserFromProjectAsync(int id)
        {
            var teamMember = await _repository.GetAsync(x=>x.Id == id,
                query=>query.Include(x=>x.User)
                .Include(x=>x.Project).ThenInclude(y=>y.PTasks));
            if (teamMember == null) Response<NoContent>.Fail("İlgili takım arkadaşı bulunamadı");
            await _repository.ClearTeamMembersTaksAsync(teamMember.UserId, teamMember.ProjectId);
            await _repository.HardDeleteAsync(teamMember);
            return Response<NoContent>.Success();
        }
        public async Task<Response<TeamMemberViewModel>> GetTeamMemberByIdAsync(int id)
        {
            var teamMember = await _repository.GetAsync(x=> x.Id == id,
                query => query.Include(x => x.Project)
                .Include(y=>y.User));
            if (teamMember == null) Response<NoContent>.Fail("İlgili takım arkadaşı bulunamadı");
            var result = new TeamMemberViewModel
            {
                Id = teamMember.Id,
                ProjectRole = teamMember.ProjectRole,
                ProjectId = teamMember.ProjectId,
                UserId = teamMember.UserId,
                UserName = teamMember.User.UserName,
                FullName = $"{teamMember.User.FirstName} {teamMember.User.LastName}",
            };
            return Response<TeamMemberViewModel>.Success(result);
        }
        public async Task<Response<bool>> CheckIfExistsAsync(string userId, int projectId)
        {
            var result = await _repository.CheckIfExistsAsync(userId, projectId);
            return Response<bool>.Success(result);
        }
    }
}
