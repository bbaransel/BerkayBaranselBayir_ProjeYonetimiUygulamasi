using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

namespace Yonetimsell.Shared.ViewModels
{
    public class TeamMemberViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public int ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }
}
