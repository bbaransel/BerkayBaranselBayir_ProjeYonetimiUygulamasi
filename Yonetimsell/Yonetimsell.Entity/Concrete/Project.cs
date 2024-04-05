using Yonetimsell.Entity.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class Project : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<PTask> PTasks { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
