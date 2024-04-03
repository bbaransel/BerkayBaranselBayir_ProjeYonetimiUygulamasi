using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.ResponseViewModels;
using Yonetimsell.Shared.ViewModels.FriendshipViewModels;

namespace Yonetimsell.Business.Concrete
{
    public class FriendshipManager : IFriendshipService
    {
        private readonly IFriendshipRepository _repository;
        private readonly MapperlyConfiguration _mapperly;
        public FriendshipManager(IFriendshipRepository repository, MapperlyConfiguration mapperly)
        {
            _repository = repository;
            _mapperly = mapperly;

        }

        public async Task<Response<List<FriendshipViewModel>>> GetFriendListAsync(string userId)
        {
            var friendList = await _repository.GetFriendListByUserId(userId);
            if (friendList == null) Response<NoContent>.Fail("Arkadaş listenizde kullanıcı bulunamadı");
            //var result = _mapperly.ListFriendshipToListFriendshipViewModel(friendList);
            var result = friendList.Select(x=> new FriendshipViewModel
            {
                Id = x.Id,
                SenderUserId = x.SenderUserId,
                SenderUserName = x.SenderUser.UserName,
                ReceiverUserId =x.ReceiverUserId,
                ReceiverUserName = x.ReceiverUser.UserName,
            }).ToList();
            return Response<List<FriendshipViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> RemoveFriendAsync(string userId, string removedUserId)
        {
            var friendship = await _repository.GetAsync(x=>x.SenderUserId == userId && x.ReceiverUserId == removedUserId);
            if (friendship == null)
            {
                friendship = await _repository.GetAsync(x => x.SenderUserId == removedUserId && x.ReceiverUserId == userId);
                if (friendship == null) Response<NoContent>.Fail("Arkadaş listenizde ilgili kullanıcı bulunamadı");
            }
            await _repository.HardDeleteAsync(friendship);
            return Response<NoContent>.Success();
        }

        public async Task<Response<FriendshipViewModel>> ReplyFriendRequestAsync(FriendshipViewModel friendshipViewModel)
        {
            var friendship = _mapperly.FriendshipViewModelToFriendship(friendshipViewModel);
            if (friendship == null) Response<NoContent>.Fail("İlgili arkadaşlık isteği bulunamadı");
            friendship.Status = friendshipViewModel.Status;
            await _repository.UpdateAsync(friendship);
            return Response<FriendshipViewModel>.Success(friendshipViewModel);
        }

        public async Task<Response<FriendshipViewModel>> SendFriendRequestAsync(FriendshipViewModel friendshipViewModel)
        {
            var friendship = _mapperly.FriendshipViewModelToFriendship(friendshipViewModel);
            var createdFriendship = await _repository.CreateAsync(friendship);
            if (createdFriendship == null) Response<NoContent>.Fail("Arkadaşlık isteği gönderilemedi");
            var result = _mapperly.FriendshipToFriendshipViewModel(createdFriendship);
            return Response<FriendshipViewModel>.Success(result);
        }
    }
}
