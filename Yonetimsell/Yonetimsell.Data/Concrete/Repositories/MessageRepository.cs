using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }
    }
}
