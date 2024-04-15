using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Shared.ViewModels.FriendshipViewModels
{
    public class FriendshipViewModel
    {
        public int Id { get; set; }
        public string SenderUserId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderFullName { get; set; }
        public string SenderImageUrl { get; set; }
        public string ReceiverUserId { get; set; }
        public string ReceiverUserName { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverImageUrl { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
