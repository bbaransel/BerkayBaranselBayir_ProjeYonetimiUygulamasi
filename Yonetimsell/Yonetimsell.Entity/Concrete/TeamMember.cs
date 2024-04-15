using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }
}
