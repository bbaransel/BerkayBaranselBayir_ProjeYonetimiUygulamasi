using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext _context) : base(_context)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task ClearAllTasksFromProjectAsync(int projectId)
        {
            var deletedPTasks = await YonetimsellDbContext.PTasks.Where(pt=>pt.ProjectId == projectId).ToListAsync();
            YonetimsellDbContext.PTasks.RemoveRange(deletedPTasks);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskFromProjectAsync(int projectId, int pTaskId)
        {
            var deletedPTask = await YonetimsellDbContext.PTasks.Where(pt=>pt.ProjectId==projectId&&pt.Id==pTaskId).FirstOrDefaultAsync();
            YonetimsellDbContext.PTasks.Remove(deletedPTask);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetProjectsByUserId(string userId)
        {
            var projects = await YonetimsellDbContext.Projects.Where(p=>p.UserId==userId).Include(p=>p.PTasks).ToListAsync();
            return projects;
        }


    }
}
