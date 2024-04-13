using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.ViewModels
{
    public class PTaskFileViewModel
    {
        public int Id { get; set; }
        public int PTaskId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
