using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string ReciverId { get; set; }

        [DisplayName("Kime:")]
        public string ReciverUserName { get; set; }
        public string ReciverFullName { get; set; }
        public string ReciverImageUrl { get; set; }
        public string SenderId { get; set; }

        [DisplayName("Kimden:")]
        public string SenderUserName { get; set; }
        public string SenderFullName { get; set; }
        public string SenderImageUrl { get; set; }
        public bool IsRead { get; set; }
        public int RelatedId { get; set; } = 0;
    }
}
