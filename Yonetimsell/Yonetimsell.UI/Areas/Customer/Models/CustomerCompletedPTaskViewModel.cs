using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.UI.Areas.Customer.Models
{
    public class CustomerCompletedPTaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
