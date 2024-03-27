using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ViewModels;

namespace Yonetimsell.Business.Mappings
{
    [Mapper(UseReferenceHandling = true)]
    public partial class Mapper2
    {
        [MapProperty(nameof(Project.TeamMembers), nameof(ProjectViewModel.TeamMembers))]
        [MapProperty(nameof(Project.Subscriptions), nameof(ProjectViewModel.Subscriptions))]
        [MapProperty(nameof(Project.PTasks), nameof(ProjectViewModel.PTasks))]
        public partial ProjectViewModel ProjectToProjectViewModel(Project project);
    }
}
