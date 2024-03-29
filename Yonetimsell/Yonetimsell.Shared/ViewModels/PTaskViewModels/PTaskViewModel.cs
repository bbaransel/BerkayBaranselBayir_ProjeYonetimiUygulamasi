using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

namespace Yonetimsell.Shared.ViewModels.PTaskViewModels
{
    public class PTaskViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public int ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
