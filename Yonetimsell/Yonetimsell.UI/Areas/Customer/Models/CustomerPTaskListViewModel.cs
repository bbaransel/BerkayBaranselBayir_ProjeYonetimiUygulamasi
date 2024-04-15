using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.UI.Areas.Customer.Models
{
    public class CustomerPTaskListViewModel
    {
        public List<PTaskViewModel> Low { get; set; }
        public List<PTaskViewModel> Medium { get; set; }
        public List<PTaskViewModel> High { get; set; }
        public List<PTaskViewModel> Critical { get; set; }
    }
}
