using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class ProjectRole
    {
        public string UserId { get; set; }
        public User User{ get; set; }
        public ProjectRoles Roles { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
