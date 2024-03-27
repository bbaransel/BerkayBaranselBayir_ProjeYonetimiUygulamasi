using Microsoft.AspNetCore.Identity;

namespace Yonetimsell.Entity.Concrete.Identity
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }

    }
}
