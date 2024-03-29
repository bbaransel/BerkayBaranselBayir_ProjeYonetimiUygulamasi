using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task ChangeProjectIsCompletedAsync(Project project)
        {
            project.IsCompleted = !project.IsCompleted;
            YonetimsellDbContext.Update(project);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task ChangeProjectPriorityAsync(Project project, Priority priority)
        {
            project.Priority = priority;
            YonetimsellDbContext.Update(project);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task ChangeProjectStatusAsync(Project project, Status status)
        {
            project.Status = status;
            YonetimsellDbContext.Update(project);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task ClearAllTasksFromProjectAsync(int projectId)
        {
            var deletedPTasks = await YonetimsellDbContext.PTasks.Where(pt => pt.ProjectId == projectId).ToListAsync();
            YonetimsellDbContext.PTasks.RemoveRange(deletedPTasks);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskFromProjectAsync(int projectId, int pTaskId)
        {
            var deletedPTask = await YonetimsellDbContext.PTasks.Where(pt => pt.ProjectId == projectId && pt.Id == pTaskId).FirstOrDefaultAsync();
            YonetimsellDbContext.PTasks.Remove(deletedPTask);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetProjectsByPriorityAsync(string userId, Priority priority)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p=>p.UserId==userId && p.Priority== priority).ToListAsync();
            return projects;
        }

        public async Task<List<Project>> GetProjectsByStatusAsync(string userId, Status status)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p => p.UserId == userId && p.Status == status).ToListAsync();
            return projects;
        }

        public async Task<List<Project>> GetProjectsByUserId(string userId)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p => p.UserId == userId).Include(p => p.PTasks).ToListAsync();
            return projects;
        }


    }
}
