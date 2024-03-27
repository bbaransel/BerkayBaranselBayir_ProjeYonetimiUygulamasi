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
        public PTaskRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task ChangeTaskIsCompleted(int taskId)
        {
            var task = await YonetimsellDbContext.PTasks.Where(pt=>pt.Id == taskId).FirstOrDefaultAsync();
            task.IsCompleted = !task.IsCompleted;
            YonetimsellDbContext.Update(task);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task ChangeTaskPriority(int taskId, Priority priority)
        {
            var task = await YonetimsellDbContext.PTasks.Where(pt => pt.Id == taskId).FirstOrDefaultAsync();
            task.Priority = priority;
            YonetimsellDbContext.Update(task);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task ChangeTaskStatus(int taskId, Status status)
        {
            var task = await YonetimsellDbContext.PTasks.Where(pt => pt.Id == taskId).FirstOrDefaultAsync();
            task.Status = status;
            YonetimsellDbContext.Update(task);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task<List<PTask>> GetTasksByPriorityAsync(string userId, Priority priority)
        {
            var tasks = await YonetimsellDbContext.PTasks.Where(pt=>pt.UserId == userId && pt.Priority == priority).ToListAsync();
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
