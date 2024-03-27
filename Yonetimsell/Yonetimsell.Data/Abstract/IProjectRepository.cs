using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Abstract
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsByUserId(string userId);
        Task DeleteTaskFromProjectAsync(int projectId, int pTaskId);
        Task ClearAllTasksFromProjectAsync(int projectId);
        Task ChangeProjectIsCompleted(int projectId);
        Task ChangeProjectStatus(int projectId, Status status);
        Task ChangeProjectPriority(int projectId, Priority priority);
        Task<List<Project>> GetProjectsByPriorityAsync(string userId, Priority priority);
        Task<List<Project>> GetProjectsByStatusAsync(string userId, Status status);
    }
}
