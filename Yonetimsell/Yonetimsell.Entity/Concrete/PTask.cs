using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Abstract;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class PTask:BaseEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
