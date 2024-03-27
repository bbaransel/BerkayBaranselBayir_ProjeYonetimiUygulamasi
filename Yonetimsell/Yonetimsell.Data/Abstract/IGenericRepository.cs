using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Yonetimsell.Data.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task HardDeleteAsync(TEntity entity);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> options = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);


    }
}
