using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class PTaskFileRepository : GenericRepository<PTaskFile>, IPTaskFileRepository
    {
        public PTaskFileRepository(YonetimsellDbContext _context) : base(_context)
        {
        }
        private YonetimsellDbContext YonetimsellDbContext
        {
            get { return _dbContext as YonetimsellDbContext; }
        }
    }
}
