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
            var deletedPTasks = await YonetimsellDbContext.PTasks.Where(pt => pt.ProjectId == projectId)
                .ToListAsync();
            YonetimsellDbContext.PTasks.RemoveRange(deletedPTasks);
            await YonetimsellDbContext.SaveChangesAsync();
        }
        public async Task ClearAllTeamMembersFromProjectAsync(int projectId)
        {
            var deletedTeamMembers = await YonetimsellDbContext.TeamMembers.Where(x=>x.ProjectId == projectId).ToListAsync();
            YonetimsellDbContext.TeamMembers.RemoveRange(deletedTeamMembers);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskFromProjectAsync(int projectId, int pTaskId)
        {
            var deletedPTask = await YonetimsellDbContext.PTasks.Where(pt => pt.ProjectId == projectId && pt.Id == pTaskId)
                .FirstOrDefaultAsync();
            YonetimsellDbContext.PTasks.Remove(deletedPTask);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetProjectsByPriorityAsync(string userId, Priority priority)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p=>p.UserId==userId && p.Priority== priority && p.IsDeleted == false)
                .ToListAsync();
            return projects;
        }

        public async Task<List<Project>> GetProjectsByStatusAsync(string userId, Status status)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p => p.UserId == userId && p.Status == status && p.IsDeleted == false)
                .ToListAsync();
            return projects;
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(string userId)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p => p.UserId == userId && p.IsDeleted==false)
                .Include(p => p.PTasks)
                .ToListAsync();
            return projects;
        }
        public async Task<List<Project>> GetDeletedProjectsByUserIdAsync(string userId)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p => p.UserId == userId && p.IsDeleted == true)
                .Include(p => p.PTasks)
                .ToListAsync();
            return projects;
        }
    }
}
