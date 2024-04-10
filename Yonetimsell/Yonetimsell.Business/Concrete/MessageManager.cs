using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.MessageViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly MapperlyConfiguration _mapperly;

        public MessageManager(IMessageRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;
        }

        public async Task<Response<MessageViewModel>> CreateAsync(MessageViewModel messageViewModel)
        {
            var message = _mapperly.MessageViewModelToMessage(messageViewModel);
            var createdMessage = await _repository.CreateAsync(message);
            if (createdMessage == null)
            {
                return Response<MessageViewModel>.Fail("Mesaj gönderilemedi");
            }
            var createdMessageViewModel = new MessageViewModel
            {
                Id = createdMessage.Id,
                IsRead = createdMessage.IsRead,
                ReciverId = createdMessage.ReciverId,
                ReciverFullName = $"{createdMessage.ReciverUser.FirstName} {createdMessage.ReciverUser.LastName}",
                ReciverUserName = createdMessage.ReciverUser.UserName,
                RelatedId = createdMessage.RelatedId,
                SenderId = createdMessage.SenderId,
                SenderFullName = $"{createdMessage.SenderUser.FirstName} {createdMessage.SenderUser.LastName}",
                SenderUserName = createdMessage.SenderUser.UserName,
                SendingDate = createdMessage.SendingDate,
                Text = createdMessage.Text
            };
            return Response<MessageViewModel>.Success(createdMessageViewModel);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string reciverId, bool isRead)
        {
            var messageList = await _repository.GetAllAsync(x => x.ReciverId == reciverId && x.IsRead == isRead);
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
                ReciverId = x.ReciverId,
                ReciverFullName = $"{x.ReciverUser.FirstName} {x.ReciverUser.LastName}",
                ReciverUserName = x.ReciverUser.UserName,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string reciverId)
        {
            var messageList = await _repository.GetAllAsync(x => x.ReciverId == reciverId);
            if (messageList.Count == 0)
            {
                return Response<List<MessageViewModel>>.Fail($"Hiç mesajınız bulunmamaktadır.");
            }
            messageList = messageList.OrderByDescending(x => x.SendingDate).ToList();
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReciverId = x.ReciverId,
                ReciverFullName = $"{x.ReciverUser.FirstName} {x.ReciverUser.LastName}",
                ReciverUserName = x.ReciverUser.UserName,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllRelatedMessagesAsync(int relatedId)
        {
            var messageList = await _repository.GetAllAsync(x => x.RelatedId == relatedId);
            if (messageList.Count == 0)
            {
                return Response<List<MessageViewModel>>.Fail("Bu konuşmaya dair başka mesaj bulunamadı.");
            }
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReciverId = x.ReciverId,
                ReciverFullName = $"{x.ReciverUser.FirstName} {x.ReciverUser.LastName}",
                ReciverUserName = x.ReciverUser.UserName,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageViewModel>>> GetAllSentMessageAsync(string senderId)
        {
            var messageList = await _repository.GetAllAsync(x => x.SenderId == senderId);
            if (messageList.Count == 0)
            {
                return Response<List<MessageViewModel>>.Fail("Giden kutusu boş");
            }
            var messageViewModelList = messageList.Select(x => new MessageViewModel
            {
                Id = x.Id,
                IsRead = x.IsRead,
                ReciverId = x.ReciverId,
                ReciverFullName = $"{x.ReciverUser.FirstName} {x.ReciverUser.LastName}",
                ReciverUserName = x.ReciverUser.UserName,
                RelatedId = x.RelatedId,
                SenderId = x.SenderId,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderUserName = x.SenderUser.UserName,
                SendingDate = x.SendingDate,
                Text = x.Text
            }).ToList();
            return Response<List<MessageViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<MessageViewModel>> GetByIdAsync(int id)
        {
            var message = await _repository.GetAsync(x => x.Id == id);
            if (message == null)
            {
                return Response<MessageViewModel>.Fail("Mesaj açılamadı");
            }
            var messageViewModel = new MessageViewModel
            {
                Id = message.Id,
                IsRead = message.IsRead,
                ReciverId = message.ReciverId,
                ReciverFullName = $"{message.ReciverUser.FirstName} {message.ReciverUser.LastName}",
                ReciverUserName = message.ReciverUser.UserName,
                RelatedId = message.RelatedId,
                SenderId = message.SenderId,
                SenderFullName = $"{message.SenderUser.FirstName} {message.SenderUser.LastName}",
                SenderUserName = message.SenderUser.UserName,
                SendingDate = message.SendingDate,
                Text = message.Text
            };
            return Response<MessageViewModel>.Success(messageViewModel);
        }

        public async Task<Response<int>> GetUserMessageCountAsync(string userId, bool isRead = false)
        {
            var count = await _repository.GetCountAsync(x => x.ReciverId == userId && x.IsRead == isRead);
            return Response<int>.Success(count);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int id)
        {
            var message = await _repository.GetAsync(x => x.Id == id);
            if (message == null)
            {
                return Response<NoContent>.Fail("Silinecek mesaj bulunamadı");
            }
            await _repository.HardDeleteAsync(message);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ChangeIsRead(int id)
        {
            var message = await _repository.GetAsync(x => x.Id == id);
            message.IsRead = true;
            await _repository.UpdateAsync(message);
            return Response<NoContent>.Success();
        }
    }
}
