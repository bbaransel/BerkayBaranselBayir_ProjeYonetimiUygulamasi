using Riok.Mapperly.Abstractions;
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
