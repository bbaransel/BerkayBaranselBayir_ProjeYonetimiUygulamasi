using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yonetimsell.Shared.Helpers.Abstract
{
    public interface ISweetAlertService
    {
        public string TopEndNotification(string icon, string title);
        public string MiddleNotification(string icon, string title);
    }
}
