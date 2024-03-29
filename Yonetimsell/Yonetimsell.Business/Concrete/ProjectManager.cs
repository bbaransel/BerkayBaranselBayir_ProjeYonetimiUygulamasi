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
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly MapperlyConfiguration _mapperly;
        public ProjectManager(IProjectRepository projectRepository, MapperlyConfiguration mapperly)
        {
            _projectRepository = projectRepository;
            _mapperly = mapperly;
        }

        public async Task<Response<NoContent>> ChangeIsCompletedAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(x=>x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _projectRepository.ChangeProjectIsCompletedAsync(project);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangeProjectPriorityAsync(int projectId, Priority priority)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _projectRepository.ChangeProjectPriorityAsync(project, priority);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangeProjectStatusAsync(int projectId, Status status)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _projectRepository.ChangeProjectStatusAsync(project, status);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ClearAllTasksAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("ilgili proje bulunamadı");
            await _projectRepository.ClearAllTasksFromProjectAsync(projectId);
            return Response<NoContent>.Success();
        }

        public async Task<Response<ProjectViewModel>> CreateAsync(AddProjectViewModel addProjectViewModel)
        {
            //var project = _mapper.Map<Project>(addProjectViewModel);
            var project = _mapperly.AddProjectViewModelToProject(addProjectViewModel);
            
            var createdProject = await _projectRepository.CreateAsync(project);
            if (createdProject == null) Response<ProjectViewModel>.Fail("Proje oluşturulamadı! Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            var result = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(result);
        }

        public async Task<Response<List<ProjectViewModel>>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<ProjectViewModel>> GetByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(p => p.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            var projectViewModel = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(projectViewModel);
        }

        public async Task<Response<List<ProjectViewModel>>> GetProjectsByPriorityAsync(string userId, Priority priority)
        {
            var projects = await _projectRepository.GetProjectsByPriorityAsync(userId,priority);
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<List<ProjectViewModel>>> GetProjectsByStatusAsync(string userId, Status status)
        {
            var projects = await _projectRepository.GetProjectsByStatusAsync(userId, status);
            if (projects == null) Response<NoContent>.Fail("Hiç proje bulunamadı");
            var result = _mapperly.ListProjectToListProjectViewModel(projects);
            return Response<List<ProjectViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(x=>x.Id == projectId);
            if (project == null) Response<NoContent>.Fail("İlgili proje bulunamadı");
            await _projectRepository.HardDeleteAsync(project);
            return Response<NoContent>.Success();
        }

        public Task<Response<NoContent>> SoftDeleteAsync(int projectId)
        {
            // Will add later on.
            throw new NotImplementedException();
        }

        public async Task<Response<ProjectViewModel>> UpdateAsync(ProjectViewModel projectViewModel)
        {
            var project = _mapperly.ProjectViewModelToProject(projectViewModel);
            if (project == null) Response<ProjectViewModel>.Fail("İlgili proje bulunamadı");
            project.ModifiedDate = DateTime.Now;
            await _projectRepository.UpdateAsync(project);
            var result = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(result);
        }
    }
}
