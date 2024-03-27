using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Abstract
{
    public interface IPTaskRepository : IGenericRepository<PTask>
    {
        Task<List<PTask>> GetTasksByUserIdAsync(string userId);
        Task ChangeTaskStatus(int  taskId, Status status);
        Task ChangeTaskPriority(int  taskId, Priority priority);
        Task ChangeTaskIsCompleted(int taskId);
        Task<List<PTask>> GetTasksByPriorityAsync(string userId, Priority priority);
        Task<List<PTask>> GetTasksByStatusAsync(string userId, Status status);

    }
}
