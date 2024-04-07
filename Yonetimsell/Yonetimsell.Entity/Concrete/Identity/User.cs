using Microsoft.AspNetCore.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; } = "/files/images/default.png";
        public DateTime? DateOfBirth { get; set; }
        public List<PTask> AssignedTasks { get; set; }
        public List<TeamMember> TeamMemberships { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
