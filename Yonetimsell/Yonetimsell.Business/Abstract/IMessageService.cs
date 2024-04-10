using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.MessageViewModels;

namespace Yonetimsell.Business.Abstract
{
    public interface IMessageService
    {
        Task<Response<MessageViewModel>> CreateAsync(MessageViewModel messageViewModel);
        Task<Response<NoContent>> HardDeleteAsync(int id);
        Task<Response<List<MessageViewModel>>> GetAllSentMessageAsync(string senderId);
        Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string reciverId, bool isRead);
        Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string reciverId);
        Task<Response<List<MessageViewModel>>> GetAllRelatedMessagesAsync(int relatedId);
        Task<Response<MessageViewModel>> GetByIdAsync(int id);
        Task<Response<int>> GetUserMessageCountAsync(string userId, bool isRead = false);
        Task<Response<NoContent>> ChangeIsRead(int id);
    }
}
