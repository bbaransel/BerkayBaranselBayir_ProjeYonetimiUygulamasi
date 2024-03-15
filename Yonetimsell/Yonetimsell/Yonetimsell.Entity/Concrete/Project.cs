using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Abstract;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class Project:BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime CompletionDate { get; set; }
        public DateTime Timeline { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public decimal Budget { get; set; }
        public List<PTask> Tasks { get; set; }
    }
}
