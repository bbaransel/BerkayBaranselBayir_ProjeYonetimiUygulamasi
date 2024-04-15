using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Abstract
{
    public interface ITeamMemberRepository : IGenericRepository<TeamMember>
    {
        Task<List<TeamMember>> GetMembersByProjectIdAsync(int projectId);
        Task<List<TeamMember>> GetMembersByUserIdAsync(string userId);
        Task ClearTeamMembersTaksAsync(string userId, int projectId);
        Task<bool> CheckIfExistsAsync(string userId, int projectId);
    }
}
