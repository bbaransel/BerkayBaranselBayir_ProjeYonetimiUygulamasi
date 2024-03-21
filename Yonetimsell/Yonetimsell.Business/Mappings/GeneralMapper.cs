using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.Business.Mappings
{
    [Mapper]
    public partial class GeneralMapper
    {
        public partial AdminUserViewModel UserToAdminUserViewModel(User user);
    }
}
