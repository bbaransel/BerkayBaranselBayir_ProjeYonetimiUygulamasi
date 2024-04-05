using AutoMapper;
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
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly MapperlyConfiguration _mapperly;

        public ProjectManager(IProjectRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;
        }

        public async Task<Response<NoContent>> ChangeProjectPriorityAsync(int projectId, Priority priority)
        {
            var project = await _repository.GetAsync(x => x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _repository.ChangeProjectPriorityAsync(project, priority);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangeProjectStatusAsync(int projectId, Status status)
        {
            var project = await _repository.GetAsync(x => x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _repository.ChangeProjectStatusAsync(project, status);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ClearAllTasksAsync(int projectId)
        {
            var project = await _repository.GetAsync(x => x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("ilgili proje bulunamadı");
            await _repository.ClearAllTasksFromProjectAsync(projectId);
            return Response<NoContent>.Success();
        }

        public async Task<Response<ProjectViewModel>> CreateAsync(AddProjectViewModel addProjectViewModel)
        {
            var project = _mapperly.AddProjectViewModelToProject(addProjectViewModel);   
            var createdProject = await _repository.CreateAsync(project);
            if (createdProject == null) Response<ProjectViewModel>.Fail("Proje oluşturulamadı! Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            var result = _mapperly.ProjectToProjectViewModel(createdProject);
            return Response<ProjectViewModel>.Success(result);
        }

        public async Task<Response<List<ProjectViewModel>>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<ProjectViewModel>> GetByIdAsync(int projectId)
        {
            var project = await _repository.GetAsync(p => p.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            var projectViewModel = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(projectViewModel);
        }

        public async Task<Response<List<ProjectViewModel>>> GetDeletedProjectsByUserIdAsync(string userId)
        {
            var projects = await _repository.GetDeletedProjectsByUserIdAsync(userId);
            if (projects == null) Response<NoContent>.Fail("Silinmiş proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<int>> GetAllProjectCountAsync()
        {
            var count = await _repository.GetCountAsync();
            return Response<int>.Success(count);
        }
        public async Task<Response<int>> GetActiveProjectCountAsync()
        {
            var count = await _repository.GetCountAsync(x=>x.Status != Status.Done && x.IsDeleted==false);
            return Response<int>.Success(count);
        }
        public async Task<Response<int>> GetCompletedProjectCountAsync()
        {
            var count = await _repository.GetCountAsync(x => x.Status == Status.Done && x.IsDeleted == false);
            return Response<int>.Success(count);
        }

        public async Task<Response<int>> GetActiveProjectCountByUserIdAsync(string userId)
        {
            var count = await _repository.GetCountAsync(x=>x.UserId == userId && x.Status!=Status.Done && x.IsDeleted == false);
            return Response<int>.Success(count);
        }

        public async Task<Response<List<ProjectViewModel>>> GetProjectsByPriorityAsync(string userId, Priority priority)
        {
            var projects = await _repository.GetProjectsByPriorityAsync(userId,priority);
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<List<ProjectViewModel>>> GetProjectsByStatusAsync(string userId, Status status)
        {
            var projects = await _repository.GetProjectsByStatusAsync(userId, status);
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<List<ProjectViewModel>>> GetProjectsByUserIdAsync(string userId)
        {
            var projects = await _repository.GetProjectsByUserIdAsync(userId);
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı.");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int projectId)
        {
            var project = await _repository.GetAsync(x=>x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _repository.HardDeleteAsync(project);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> SoftDeleteAsync(int projectId)
        {
            var project = await _repository.GetAsync(x=> x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            project.IsDeleted = !project.IsDeleted;
            project.ModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(project);
            return Response<NoContent>.Success();
        }

        public async Task<Response<ProjectViewModel>> UpdateAsync(ProjectViewModel projectViewModel)
        {
            var project = _mapperly.ProjectViewModelToProject(projectViewModel);
            if (project == null) Response<ProjectViewModel>.Fail("İlgili proje bulunamadı");
            project.ModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(project);
            var result = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(result);
        }

        public async Task<Response<int>> GetCompletedProjectCountByUserIdAsync(string userId)
        {
            var count = await _repository.GetCountAsync(x=>x.UserId==userId && x.Status==Status.Done && x.IsDeleted==false);
            return Response<int>.Success(count);
        }
    }
}
