using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Abstract
{
    public interface IPTaskRepository : IGenericRepository<PTask>
    {
        Task<List<PTask>> GetTasksByUserIdAsync(string userId);
        Task<List<PTask>> GetTasksByProjectIdAsync(int projectId);
        Task ChangeTaskStatusAsync(PTask pTask, Status status);
        Task ChangeTaskPriorityAsync(PTask pTask, Priority priority);
        Task<List<PTask>> GetTasksByPriorityAsync(string userId, Priority priority);
        Task<List<PTask>> GetTasksByStatusAsync(string userId, Status status);

    }
}
