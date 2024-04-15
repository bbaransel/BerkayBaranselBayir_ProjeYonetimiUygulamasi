using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.MessageViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly MapperlyConfiguration _mapperly;
        private readonly UserManager<User> _userManager;

        public MessageManager(IMessageRepository repository, MapperlyConfiguration mapperly, UserManager<User> userManager)
        {
            _repository = repository;
            _mapperly = mapperly;
            _userManager = userManager;
        }

        public async Task<Response<MessageViewModel>> CreateAsync(MessageViewModel messageViewModel)
        {
            var message = _mapperly.MessageViewModelToMessage(messageViewModel);
            message.ReceiverUser = await _userManager.FindByIdAsync(message.ReceiverId);
            message.SenderUser = await _userManager.FindByIdAsync(message.SenderId);
            var createdMessage = await _repository.CreateAsync(message);
            if (createdMessage == null)
            {
                return Response<MessageViewModel>.Fail("Mesaj gönderilemedi");
            }
            var createdMessageViewModel = new MessageViewModel
            {
                Id = createdMessage.Id,
                IsRead = createdMessage.IsRead,
                ReceiverId = createdMessage.ReceiverId,
                ReceiverFullName = $"{createdMessage.ReceiverUser.FirstName} {createdMessage.ReceiverUser.LastName}",
                ReceiverUserName = createdMessage.ReceiverUser.UserName,
                ReceiverImageUrl = createdMessage.ReceiverUser.ImageUrl,
                RelatedId = createdMessage.RelatedId,
                SenderId = createdMessage.SenderId,
                SenderFullName = $"{createdMessage.SenderUser.FirstName} {createdMessage.SenderUser.LastName}",
                SenderUserName = createdMessage.SenderUser.UserName,
                SenderImageUrl = createdMessage.SenderUser.ImageUrl,
                SendingDate = createdMessage.SendingDate,
                Text = createdMessage.Text
            };
            return Response<MessageViewModel>.Success(createdMessageViewModel);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string reciverId, bool isRead)
        {
            var messageList = await _repository.GetAllAsync(x => x.ReceiverId == reciverId && x.IsRead == isRead,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (messageList.Count == 0)
            {
                var infoText = isRead ? "Okunmuş" : "Okunmamış";
                return Response<List<MessageViewModel>>.Fail($"{infoText} mesajınız bulunmamaktadır.");
            }
            messageList = messageList.OrderByDescending(x => x.SendingDate).ToList();
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReceiverId = x.ReceiverId,
                ReceiverFullName = $"{x.ReceiverUser.FirstName} {x.ReceiverUser.LastName}",
                ReceiverUserName = x.ReceiverUser.UserName,
                ReceiverImageUrl = x.ReceiverUser.ImageUrl,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SenderImageUrl = x.SenderUser.ImageUrl,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string reciverId)
        {
            var messageList = await _repository.GetAllAsync(x => x.ReceiverId == reciverId,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (messageList.Count == 0)
            {
                return Response<List<MessageViewModel>>.Fail($"Hiç mesajınız bulunmamaktadır.");
            }
            messageList = messageList.OrderByDescending(x => x.SendingDate).ToList();
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReceiverId = x.ReceiverId,
                ReceiverFullName = $"{x.ReceiverUser.FirstName} {x.ReceiverUser.LastName}",
                ReceiverUserName = x.ReceiverUser.UserName,
                ReceiverImageUrl = x.ReceiverUser.ImageUrl,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SenderImageUrl = x.SenderUser.ImageUrl,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllRelatedMessagesAsync(int relatedId)
        {
            var messageList = await _repository.GetAllAsync(x => x.RelatedId == relatedId,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (messageList.Count == 0)
            {
                return Response<List<MessageViewModel>>.Fail("Bu konuşmaya dair başka mesaj bulunamadı.");
            }
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReceiverId = x.ReceiverId,
                ReceiverFullName = $"{x.ReceiverUser.FirstName} {x.ReceiverUser.LastName}",
                ReceiverUserName = x.ReceiverUser.UserName,
                ReceiverImageUrl = x.ReceiverUser.ImageUrl,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SenderImageUrl = x.SenderUser.ImageUrl,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            var firstMessage = await _repository.GetAsync(x => x.Id == relatedId,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            var firstMessageViewModel = new MessageViewModel
            {
                Id = firstMessage.Id,
                IsRead = firstMessage.IsRead,
                ReceiverId = firstMessage.ReceiverId,
                ReceiverFullName = $"{firstMessage.ReceiverUser.FirstName} {firstMessage.ReceiverUser.LastName}",
                ReceiverUserName = firstMessage.ReceiverUser.UserName,
                ReceiverImageUrl = firstMessage.ReceiverUser.ImageUrl,
                RelatedId = firstMessage.RelatedId,
                SenderId = firstMessage.SenderId,
                SenderFullName = $"{firstMessage.SenderUser.FirstName} {firstMessage.SenderUser.LastName}",
                SenderUserName = firstMessage.SenderUser.UserName,
                SenderImageUrl = firstMessage.SenderUser.ImageUrl,
                SendingDate = firstMessage.SendingDate,
                Text = firstMessage.Text
            };
            messageViewModelList.Insert(0, firstMessageViewModel);

            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllSentMessageAsync(string senderId)
        {
            var messageList = await _repository.GetAllAsync(x => x.SenderId == senderId,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (messageList.Count == 0)
            {
                return Response<List<MessageViewModel>>.Fail("Giden kutusu boş");
            }
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReceiverId = x.ReceiverId,
                ReceiverFullName = $"{x.ReceiverUser.FirstName} {x.ReceiverUser.LastName}",
                ReceiverUserName = x.ReceiverUser.UserName,
                ReceiverImageUrl = x.ReceiverUser.ImageUrl,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SenderImageUrl = x.SenderUser.ImageUrl,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<MessageViewModel>> GetByIdAsync(int id)
        {
            var message = await _repository.GetAsync(x => x.Id == id, query =>
                query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (message == null)
            {
                return Response<MessageViewModel>.Fail("Mesaj açılamadı");
            }
            var messageViewModel = new MessageViewModel
            {
                Id = message.Id,
                IsRead = message.IsRead,
                ReceiverId = message.ReceiverId,
                ReceiverFullName = $"{message.ReceiverUser.FirstName} {message.ReceiverUser.LastName}",
                ReceiverUserName = message.ReceiverUser.UserName,
                ReceiverImageUrl = message.ReceiverUser.ImageUrl,
                RelatedId = message.RelatedId,
                SenderId = message.SenderId,
                SenderFullName = $"{message.SenderUser.FirstName} {message.SenderUser.LastName}",
                SenderUserName = message.SenderUser.UserName,
                SenderImageUrl = message.SenderUser.ImageUrl,
                SendingDate = message.SendingDate,
                Text = message.Text
            };
            return Response<MessageViewModel>.Success(messageViewModel);
        }

        public async Task<Response<int>> GetUserMessageCountAsync(string userId, bool isRead = false)
        {
            var count = await _repository.GetCountAsync(x => x.ReceiverId == userId && x.IsRead == isRead);
            return Response<int>.Success(count);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int id)
        {
            var message = await _repository.GetAsync(x => x.Id == id, query =>
                query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (message == null)
            {
                return Response<NoContent>.Fail("Silinecek mesaj bulunamadı");
            }
            await _repository.HardDeleteAsync(message);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangeIsRead(int id)
        {
            var message = await _repository.GetAsync(x => x.Id == id, query =>
                query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            message.IsRead = true;
            await _repository.UpdateAsync(message);
            return Response<NoContent>.Success();
        }
    }
}
