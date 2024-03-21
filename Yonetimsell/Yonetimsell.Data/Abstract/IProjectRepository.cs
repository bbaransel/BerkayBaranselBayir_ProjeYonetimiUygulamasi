using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Abstract
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsByUserId(string userId);
        Task DeleteTaskFromProjectAsync(int projectId, int pTaskId);
        Task ClearAllTasksFromProjectAsync(int projectId);
    }
}
