using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ViewModels
{
    public class ProgressTime
    {
        public double DesignatedDays { get; set; }
        public double PassingDays { get; set; }
        public double RemainingDays { get; set; }
        public double ProgressedDaysPercentage { get; set; }
    }
}
