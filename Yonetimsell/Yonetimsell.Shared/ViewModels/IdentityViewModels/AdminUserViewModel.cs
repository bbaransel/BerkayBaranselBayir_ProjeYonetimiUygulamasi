namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class AdminUserViewModel
    {
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<AssignRolesViewModel> Roles { get; set; }
    }
}
