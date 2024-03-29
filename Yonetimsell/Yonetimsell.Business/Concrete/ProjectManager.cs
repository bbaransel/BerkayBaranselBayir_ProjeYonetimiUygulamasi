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
        private readonly IMapper _mapper;
        private readonly MapperlyConfiguration _mapperly;
        public ProjectManager(IProjectRepository projectRepository, IMapper mapper, MapperlyConfiguration mapperly)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _mapperly = mapperly;
        }

        public Task<Response<NoContent>> ChangeIsCompletedAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> ChangeProjectPriorityAsync(int projectId, Priority priority)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> ChangeProjectStatusAsync(int projectId, Status status)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> ClearAllTasksAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProjectViewModel>> CreateAsync(AddProjectViewModel addProjectViewModel)
        {
            //var project = _mapper.Map<Project>(addProjectViewModel);
            var project = _mapperly.AddProjectViewModelToProject(addProjectViewModel);
            
            var createdProject = await _projectRepository.CreateAsync(project);
            if (createdProject == null)
            {
              return Response<ProjectViewModel>.Fail("Proje oluşturulamadı! Sorunun devam etmesi durumunda Yönetici ile iletişime geçiniz.");
            }
            var result = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(result);
        }

        public Task<Response<List<ProjectViewModel>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProjectViewModel>> GetByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(p => p.Id == projectId);
            var projectViewModel = _mapperly.ProjectToProjectViewModel(project);
            return Response<ProjectViewModel>.Success(projectViewModel);
        }

        public Task<Response<List<ProjectViewModel>>> GetProjectsByPriorityAsync(string userId, Priority priority)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<ProjectViewModel>>> GetProjectsByStatusAsync(string userId, Status status)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> HardDeleteAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> SoftDeleteAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProjectViewModel>> UpdateAsync(ProjectViewModel projectViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
