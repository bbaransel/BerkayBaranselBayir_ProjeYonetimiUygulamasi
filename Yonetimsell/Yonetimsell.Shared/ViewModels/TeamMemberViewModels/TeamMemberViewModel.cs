using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.TeamMemberViewModels
{
    public class TeamMemberViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
        public string FullName { get; set; }
        public int ProjectId { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }
}
