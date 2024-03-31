using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Entity.Concrete
{
    public class Friendship
    {
        public int Id { get; set; }
        public string SenderUserId { get; set; }
        public User SenderUser { get; set; }
        public string ReceiverUserId { get; set; }
        public User ReceiverUser { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
