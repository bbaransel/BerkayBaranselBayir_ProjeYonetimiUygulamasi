using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Repositories
{
    public class PTaskRepository : GenericRepository<PTask>, IPTaskRepository
    {
        public PTaskRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public YonetimsellDbContext YonetimsellDbContext
        {
            get{ return _dbContext as  YonetimsellDbContext; }
        }
    }
}
