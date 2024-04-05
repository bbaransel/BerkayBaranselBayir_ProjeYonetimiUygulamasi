using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class PTaskRepository : GenericRepository<PTask>, IPTaskRepository
    {
        public PTaskRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        public YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task ChangeTaskPriorityAsync(PTask pTask, Priority priority)
        {
            pTask.Priority = priority;
            YonetimsellDbContext.Update(pTask);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task ChangeTaskStatusAsync(PTask pTask, Status status)
        {
            pTask.Status = status;
            YonetimsellDbContext.Update(pTask);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task<List<PTask>> GetTasksByPriorityAsync(string userId, Priority priority)
        {
            var tasks = await YonetimsellDbContext.PTasks.Where(pt=>pt.UserId == userId && pt.Priority == priority && pt.Status!=Status.Done).ToListAsync();
            return tasks;
        }

        public async Task<List<PTask>> GetTasksByProjectIdAsync(int projectId)
        {
            var tasks = await YonetimsellDbContext.PTasks.Where(pt=>pt.ProjectId == projectId).ToListAsync(); 
            return tasks;
        }

        public async Task<List<PTask>> GetTasksByStatusAsync(string userId, Status status)
        {
            var tasks = await YonetimsellDbContext.PTasks.Where(pt => pt.UserId == userId && pt.Status == status).ToListAsync();
            return tasks;
        }

        public async Task<List<PTask>> GetTasksByUserIdAsync(string userId)
        {
            var tasks = await YonetimsellDbContext.PTasks.Where(pt => pt.UserId == userId).ToListAsync();
            return tasks;
        }
    }
}
