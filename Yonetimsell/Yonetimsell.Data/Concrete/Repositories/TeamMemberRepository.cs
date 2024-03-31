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
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(DbContext dbContext) : base(dbContext)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }

        public async Task<List<TeamMember>> GetMembersByProjectIdAsync(int projectId)
        {
            var teamMembers = await YonetimsellDbContext.TeamMembers.Where(x=> x.ProjectId == projectId).ToListAsync();
            return teamMembers;
        }

        public async Task<List<TeamMember>> GetMembersByUserIdAsync(string userId)
        {
            var teamMembers = await YonetimsellDbContext.TeamMembers.Where(x => x.UserId == userId).ToListAsync();
            return teamMembers;
        }
    }
}
