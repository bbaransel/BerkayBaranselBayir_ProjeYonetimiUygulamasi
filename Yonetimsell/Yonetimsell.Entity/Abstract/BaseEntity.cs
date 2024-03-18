using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Entity.Abstract
{
    public abstract class BaseEntity
    {
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
