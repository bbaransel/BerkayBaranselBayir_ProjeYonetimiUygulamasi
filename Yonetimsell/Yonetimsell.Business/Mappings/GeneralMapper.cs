using Riok.Mapperly.Abstractions;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.Business.Mappings
{
    [Mapper(UseReferenceHandling = true)]
    public partial class GeneralMapper
    {
        //public partial AdminUserViewModel UserToAdminUserViewModel(User user);
        //public partial ProjectViewModel ProjectToProjectViewModel(Project project);
        [MapProperty(nameof(ProjectViewModel.TeamMembers), nameof(AddProjectViewModel.TeamMembers))]
        [MapProperty(nameof(ProjectViewModel.PTasks), nameof(AddProjectViewModel.PTasks))]
        public partial ProjectViewModel AddProjectViewModelToProjectViewModel(AddProjectViewModel addProjectViewModel);

        [MapProperty(nameof(Project.TeamMembers), nameof(ProjectViewModel.TeamMembers))]
        [MapProperty(nameof(Project.Subscriptions), nameof(ProjectViewModel.Subscriptions))]
        [MapProperty(nameof(Project.PTasks), nameof(ProjectViewModel.PTasks))]
        public partial Project ProjectViewModelToProject(ProjectViewModel projectViewModel);
    }
}
