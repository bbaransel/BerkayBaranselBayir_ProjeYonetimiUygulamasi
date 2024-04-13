using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.FriendshipViewModels;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.MessageViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Business.Mappings
{
    [Mapper]
    public partial class MapperlyConfiguration
    {
        #region PTask
        public partial PTask AddPTaskViewModelToPTask(AddPTaskViewModel addPTaskViewModel);
        public partial PTask PTaskViewModelToPTask(PTaskViewModel pTaskViewModel);
        public partial List<PTask> ListPTaskViewModelToListPTask(List<PTaskViewModel> pTaskViewModels);
        public partial PTaskViewModel PTaskToPTaskViewModel(PTask pTask);
        [MapProperty(nameof(PTask.User.UserName), nameof(PTaskViewModel.UserName))]
        public partial List<PTaskViewModel> ListPTaskToListPTaskViewModel(List<PTask> pTasks);
        public partial PTask EditPTaskViewModelToPTask(EditPTaskViewModel editPTaskViewModel);
        #endregion
        #region TeamMember
        public partial TeamMember TeamMemberViewModelToTeamMember(TeamMemberViewModel teamMemberViewModel);
        public partial List<TeamMember> ListTeamMemberViewModelToListTeamMember(List<TeamMemberViewModel> teamMemberViewModels);
        public partial TeamMemberViewModel TeamMemberToTeamMemberViewModel(TeamMember teamMember);
        public partial List<TeamMemberViewModel> ListTeamMemberToListTeamMemberViewModel(List<TeamMember> teamMembers);
        #endregion
        #region Subscription
        public partial Subscription SubscriptionViewModelToSubscription(SubscriptionViewModel subscriptionViewModel);
        public partial List<Subscription> ListSubscriptionViewModelToListSubscription(List<SubscriptionViewModel> subscriptionViewModels);
        public partial SubscriptionViewModel SubscriptionToSubscriptionViewModel(Subscription subscription);
        public partial List<SubscriptionViewModel> ListSubscriptionToListSubscriptionViewModel(List<Subscription> subscriptions);
        #endregion
        #region Friendship
        public partial Friendship FriendshipViewModelToFriendship(FriendshipViewModel friendshipViewModel);
        public partial FriendshipViewModel FriendshipToFriendshipViewModel(Friendship friendship);
        public partial List<Friendship> ListFriendshipViewModelToListFriendship(List<FriendshipViewModel> friendshipViewModels);
        public partial List<FriendshipViewModel> ListFriendshipToListFriendshipViewModel(List<Friendship> friendships);
        [MapProperty(nameof(User.Id), nameof(AddFriendUserViewModel.UserId))]
        public partial AddFriendUserViewModel UserToAddFriendUserViewModel(User user);
        #endregion
        #region Project
        public partial Project AddProjectViewModelToProject(AddProjectViewModel addProjectViewModel);
        public partial ProjectViewModel ProjectToProjectViewModel(Project project);
        public partial List<ProjectViewModel> ListProjectToListProjectViewModel(List<Project> projects);
        public partial Project ProjectViewModelToProject(ProjectViewModel projectViewModel);
        public partial List<Project> ListProjectViewModelToListProject(List<ProjectViewModel> projectViewModels);
        public partial ProjectViewModel EditProjectViewModelToProjectViewModel(EditProjectViewModel editProjectViewModel);
        public partial EditProjectViewModel ProjectViewModelToEditProjectViewModel(ProjectViewModel projectViewModels);
        #endregion
        #region User
        public partial UserViewModel UserToUserViewModel(User user);
        public partial EditUserViewModel UserToEditUserViewModel(User user);
        #endregion
        #region Message
        public partial MessageViewModel MessageToMessageViewModel(Message message);
        public partial List<MessageViewModel> ListMessageToListMessageViewModel(List<Message> messages);
        public partial Message MessageViewModelToMessage(MessageViewModel messageViewModel);
        public partial List<Message> ListMessageViewModelToListMessage(List<MessageViewModel> messageViewModels);
        #endregion
        #region PTaskFile
        public partial PTaskFileViewModel PTaskFileToPTaskFileViewModel(PTaskFile pTaskFile);
        public partial List<PTaskFileViewModel> ListPTaskFileToListPTaskFileViewModel(List<PTaskFile> pTaskFiles);
        public partial PTaskFile PTaskFileViewModelToPTaskFile(PTaskFileViewModel pTaskFileViewModel);
        public partial List<PTaskFile> ListPTaskFileViewModelToListPTaskFile(List<PTaskFileViewModel> pTaskFileViewModels);
        #endregion
    }
}
