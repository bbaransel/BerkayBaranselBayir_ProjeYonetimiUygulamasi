using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Abstract
{
    public interface IFriendshipRepository:IGenericRepository<Friendship>
    {
        Task<List<Friendship>> GetFriendListByUserId(string userId);
    }
}
