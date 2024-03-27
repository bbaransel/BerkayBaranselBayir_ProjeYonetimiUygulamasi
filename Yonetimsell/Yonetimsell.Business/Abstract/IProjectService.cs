using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IProjectService
    {
        Task<Response<ProjectViewModel>> CreateAsync(AddProjectViewModel addProjectViewModel);
        Task<Response<ProjectViewModel>> UpdateAsync(ProjectViewModel projectViewModel);
        Task<Response<NoContent>> HardDeleteAsync(int projectId);
        Task<Response<NoContent>> SoftDeleteAsync(int projectId);
        Task<Response<List<ProjectViewModel>>> GetAllAsync();
        Task<Response<ProjectViewModel>> GetByIdAsync(int projectId);
        Task<Response<List<ProjectViewModel>>> GetProjectsByPriorityAsync(string userId, Priority priority);
        Task<Response<List<ProjectViewModel>>> GetProjectsByStatusAsync(string userId, Status status);
        Task<Response<NoContent>> ClearAllTasksAsync(int projectId);
        Task<Response<NoContent>> ChangeIsCompletedAsync(int projectId);
        Task<Response<NoContent>> ChangeProjectStatusAsync(int projectId, Status status);
        Task<Response<NoContent>> ChangeProjectPriorityAsync(int projectId, Priority priority);
    


    }
}
