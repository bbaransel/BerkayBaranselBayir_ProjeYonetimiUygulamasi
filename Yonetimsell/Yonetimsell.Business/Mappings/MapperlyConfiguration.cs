using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

namespace Yonetimsell.Business.Mappings
{
    [Mapper]
    public partial class MapperlyConfiguration
    {
        #region Project
        public partial Project AddProjectViewModelToProject(AddProjectViewModel addProjectViewModel);
        public partial ProjectViewModel ProjectToProjectViewModel(Project project);
        public partial List<ProjectViewModel> ListProjectToListProjectViewModel(List<Project> projects);
        public partial Project ProjectViewModelToProject(ProjectViewModel projectViewModel);
        public partial List<Project> ListProjectViewModelToListProject(List<ProjectViewModel> projectViewModels);
        #endregion
        #region PTask
        public partial PTask AddPTaskViewModelToPTask(AddPTaskViewModel addPTaskViewModel);
        public partial PTask PTaskViewModelToPTask(PTaskViewModel pTaskViewModel);
        public partial List<PTask> ListPTaskViewModelToListPTask(List<PTaskViewModel> pTaskViewModels);
        public partial PTaskViewModel PTaskToPTaskViewModel(PTask pTask);
        public partial List<PTaskViewModel> ListPTaskToListPTaskViewModel(List<PTask> pTasks);
        #endregion
    }
}
