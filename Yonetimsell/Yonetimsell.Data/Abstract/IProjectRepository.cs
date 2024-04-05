using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Abstract
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsByUserIdAsync(string userId);
        Task DeleteTaskFromProjectAsync(int projectId, int pTaskId);
        Task ClearAllTasksFromProjectAsync(int projectId);
        Task ChangeProjectStatusAsync(Project project, Status status);
        Task ChangeProjectPriorityAsync(Project project, Priority priority);
        Task<List<Project>> GetProjectsByPriorityAsync(string userId, Priority priority);
        Task<List<Project>> GetProjectsByStatusAsync(string userId, Status status);
        Task<List<Project>> GetDeletedProjectsByUserIdAsync(string userId);
    }
}
