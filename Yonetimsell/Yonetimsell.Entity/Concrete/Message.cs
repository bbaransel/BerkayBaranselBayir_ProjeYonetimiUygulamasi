using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.Entity.Concrete
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public User SenderUser { get; set; }
        public string ReceiverId { get; set; }
        public User ReceiverUser { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendingDate { get; set; } = DateTime.Now;
        public int RelatedId { get; set; } = 0;
    }
}
