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
    [Mapper]
    public partial class MapperlyConfiguration
    {
        public partial Project AddProjectViewModelToProject(AddProjectViewModel addProjectViewModel);
        public partial AddProjectViewModel ProjectToAddProjectViewModel(Project project);
        public partial ProjectViewModel ProjectToProjectViewModel(Project project);
        public partial List<ProjectViewModel> ListProjectToListProjectViewModel(List<Project> projects);
        public partial Project ProjectViewModelToProject(ProjectViewModel projectViewModel);
        public partial List<Project> ListProjectViewModelToListProject(List<ProjectViewModel> projectViewModels);
    }
}
