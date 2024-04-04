using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.UI.Areas.Customer.Models
{
    public class CustomerPTaskListViewModel
    {
        public List<PTaskViewModel> PTasks { get; set; }
        public List<PTaskViewModel> Low
        {
            get 
            {
                var tasks = PTasks.Where(x=>x.Priority== Priority.Low).ToList();
                return tasks;
            }
        }
        public List<PTaskViewModel> Medium
        { 
            get
            {
                var tasks = PTasks.Where(x => x.Priority == Priority.Medium).ToList();
                return tasks;
            }
        }
        public List<PTaskViewModel> High
        {
            get
            {
                var tasks = PTasks.Where(x => x.Priority == Priority.High).ToList();
                return tasks;
            }
        }
        public List<PTaskViewModel> Critical
        {
            get
            {
                var tasks = PTasks.Where(x => x.Priority == Priority.Critical).ToList();
                return tasks;
            }
        }
    }
}
