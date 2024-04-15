using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Yonetimsell.Shared.ViewModels.MessageViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Tarih/Saat:")]
        public DateTime SendingDate { get; set; } = DateTime.Now;

        [DisplayName("Mesaj:")]
        [Required(ErrorMessage = "Mesaj boş bırakılmamalıdır.")]
        public string Text { get; set; }

        [DisplayName("Yanıt:")]
        public string ReceiverId { get; set; }

        [DisplayName("Kime:")]
        public string ReceiverUserName { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverImageUrl { get; set; }
        public string SenderId { get; set; }

        [DisplayName("Kimden:")]
        public string SenderUserName { get; set; }
        public string SenderFullName { get; set; }
        public string SenderImageUrl { get; set; }
        public bool IsRead { get; set; }
        public int RelatedId { get; set; } = 0;
    }
}
