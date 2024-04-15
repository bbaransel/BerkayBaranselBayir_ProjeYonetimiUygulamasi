using Microsoft.EntityFrameworkCore;
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

        public async Task<Response<FriendshipViewModel>> GetByIdAsync(int id)
        {
            var friendship = await _repository.GetAsync(x => x.Id == id,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (friendship == null)
            {
                return Response<FriendshipViewModel>.Fail("Geçerli istek bulunamadı");
            }
            var result = new FriendshipViewModel
            {
                Id = id,
                ReceiverUserId = friendship.ReceiverUserId,
                ReceiverFullName = $"{friendship.ReceiverUser.FirstName} {friendship.ReceiverUser.LastName}",
                ReceiverImageUrl = friendship.ReceiverUser.ImageUrl,
                ReceiverUserName = friendship.ReceiverUser.UserName,
                Status = friendship.Status,
                SenderUserName = friendship.SenderUser.UserName,
                SenderUserId = friendship.SenderUserId,
                SenderImageUrl = friendship.SenderUser.ImageUrl,
                SenderFullName = $"{friendship.SenderUser.FirstName} {friendship.SenderUser.LastName}",
            };
            return Response<FriendshipViewModel>.Success(result);
        }
        public async Task<Response<bool>> CheckIfFriendshipExistsAsync(string currentUserId, string otherUserId)
        {
            var result = await _repository.CheckIfFriendshipExistsAsync(currentUserId, otherUserId);
            return Response<bool>.Success(result);
        }

        public async Task<Response<List<FriendshipViewModel>>> GetFriendListAsync(string userId)
        {
            var friendList = await _repository.GetFriendListByUserIdAsync(userId);
            if (friendList == null)
            {
                return Response<List<FriendshipViewModel>>.Fail("Arkadaş listenizde kullanıcı bulunamadı");
            }
            var result = friendList.Select(x => new FriendshipViewModel
            {
                Id = x.Id,
                SenderUserId = x.SenderUserId,
                SenderUserName = x.SenderUser.UserName,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderImageUrl = x.SenderUser.ImageUrl,
                ReceiverUserId = x.ReceiverUserId,
                ReceiverUserName = x.ReceiverUser.UserName,
                ReceiverFullName = $"{x.ReceiverUser.FirstName} {x.ReceiverUser.LastName}",
                ReceiverImageUrl = x.ReceiverUser.ImageUrl,
            }).ToList();
            return Response<List<FriendshipViewModel>>.Success(result);
        }
        public async Task<Response<List<FriendshipViewModel>>> GetPendingFriendListAsync(string userId)
        {
            var friendList = await _repository.GetFriendListByStatusAsync(userId, FriendshipStatus.Pending);
            if (friendList == null)
            {
                return Response<List<FriendshipViewModel>>.Fail("Arkadaş listenizde kullanıcı bulunamadı");
            }
            var result = friendList.Select(x => new FriendshipViewModel
            {
                Id = x.Id,
                SenderUserId = x.SenderUserId,
                SenderUserName = x.SenderUser.UserName,
                SenderFullName = $"{x.SenderUser.FirstName} {x.SenderUser.LastName}",
                SenderImageUrl = x.SenderUser.ImageUrl,
                ReceiverUserId = x.ReceiverUserId,
                ReceiverUserName = x.ReceiverUser.UserName,
                ReceiverFullName = $"{x.ReceiverUser.FirstName} {x.ReceiverUser.LastName}",
                ReceiverImageUrl = x.ReceiverUser.ImageUrl,
            }).ToList();
            return Response<List<FriendshipViewModel>>.Success(result);
        }

        public async Task<Response<NoContent>> RemoveFriendAsync(string userId, string removedUserId)
        {
            var friendship = await _repository.GetAsync(x => x.SenderUserId == userId && x.ReceiverUserId == removedUserId,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (friendship == null)
            {
                friendship = await _repository.GetAsync(x => x.SenderUserId == removedUserId && x.ReceiverUserId == userId, query =>
                    query.Include(y => y.SenderUser)
                    .Include(y => y.ReceiverUser));
                if (friendship == null)
                {
                    return Response<NoContent>.Fail("Arkadaş listenizde ilgili kullanıcı bulunamadı");
                }
            }
            await _repository.HardDeleteAsync(friendship);
            return Response<NoContent>.Success();
        }
        public async Task<Response<NoContent>> DeleteFriendshipByIdAsync(int id)
        {
            var friendship = await _repository.GetAsync(x => x.Id == id,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (friendship == null)
            {
                if (friendship == null)
                {
                    return Response<NoContent>.Fail("Arkadaş listenizde ilgili kullanıcı bulunamadı");
                }
            }
            await _repository.HardDeleteAsync(friendship);
            return Response<NoContent>.Success();
        }

        public async Task<Response<FriendshipViewModel>> SendFriendRequestAsync(FriendshipViewModel friendshipViewModel)
        {
            var friendship = _mapperly.FriendshipViewModelToFriendship(friendshipViewModel);
            var createdFriendship = await _repository.CreateAsync(friendship);
            if (createdFriendship == null)
            {
                Response<FriendshipViewModel>.Fail("Arkadaşlık isteği gönderilemedi");
            }
            var result = _mapperly.FriendshipToFriendshipViewModel(createdFriendship);
            return Response<FriendshipViewModel>.Success(result);
        }
        public async Task<Response<NoContent>> AcceptFriendRequestAsync(int id)
        {
            var friendship = await _repository.GetAsync(x => x.Id == id,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (friendship == null)
            {
                return Response<NoContent>.Fail("İlgili arkadaşlık isteği bulunamadı");
            }
            friendship.Status = FriendshipStatus.Accepted;
            await _repository.UpdateAsync(friendship);
            return Response<NoContent>.Success();
        }
        public async Task<Response<NoContent>> DenyFriendRequestAsync(int id)
        {
            var friendship = await _repository.GetAsync(x => x.Id == id,
                query => query.Include(y => y.SenderUser)
                .Include(y => y.ReceiverUser));
            if (friendship == null)
            {
                return Response<NoContent>.Fail("İlgili arkadaşlık isteği bulunamadı");
            }
            await _repository.HardDeleteAsync(friendship);
            return Response<NoContent>.Success();
        }
    }
}
