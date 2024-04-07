using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.IdentityViewModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<AssignRolesViewModel> Roles { get; set; }
    }
}
