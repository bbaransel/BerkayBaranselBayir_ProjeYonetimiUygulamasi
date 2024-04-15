using Microsoft.EntityFrameworkCore;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task ClearTeamMembersTaksAsync(string userId, int projectId)
        {
            var deletedTask = await YonetimsellDbContext.PTasks.Where(x => x.ProjectId == projectId && x.UserId == userId).ToListAsync();
            YonetimsellDbContext.PTasks.RemoveRange(deletedTask);
            await YonetimsellDbContext.SaveChangesAsync();
        }

        public async Task<List<TeamMember>> GetMembersByProjectIdAsync(int projectId)
        {
            var teamMembers = await YonetimsellDbContext.TeamMembers.Where(x => x.ProjectId == projectId).ToListAsync();
            return teamMembers;
        }

        public async Task<List<TeamMember>> GetMembersByUserIdAsync(string userId)
        {
            var teamMembers = await YonetimsellDbContext.TeamMembers.Where(x => x.UserId == userId).ToListAsync();
            return teamMembers;
        }
        public async Task<bool> CheckIfExistsAsync(string userId, int projectId)
        {
            var teamMember = await YonetimsellDbContext.TeamMembers.Where(x => x.UserId == userId && x.ProjectId == projectId).FirstOrDefaultAsync();
            if (teamMember != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
