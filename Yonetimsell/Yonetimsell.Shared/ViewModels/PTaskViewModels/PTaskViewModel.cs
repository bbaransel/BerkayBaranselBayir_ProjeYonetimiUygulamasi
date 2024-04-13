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

        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public int ProgressTimePercentage
        {
            get
            {
                var totalDays = (int)Math.Ceiling((DueDate - CreatedDate).TotalDays);
                if (totalDays <= 0) return 100;
                var elapsedDays = (int)Math.Ceiling((DateTime.Now - CreatedDate).TotalDays);
                if (elapsedDays < 0) return 0;
                var progress = (int)Math.Ceiling((elapsedDays * 100.0) / totalDays);
                return progress;
            }
        }
    }
}
