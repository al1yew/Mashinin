using System.Linq.Expressions;

namespace Mashinin.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<List<TEntity>> GetAllAsync(params string[] includes);
        Task<List<TEntity>> GetAllByExAsync(Expression<Func<TEntity, bool>> filters, params string[] includes);
        Task<List<TResult>> GetSelectedByExAsync<TResult>(
           Expression<Func<TEntity, TResult>> selector,
           Expression<Func<TEntity, bool>> filter,
           params string[] includes);
        Task<List<TResult>> GetFilteredAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> ex, params string[] includes);
        void Remove(TEntity entity);
        Task<bool> DoesExistAsync(Expression<Func<TEntity, bool>> ex);
        void UpdateAsync(TEntity entity);
    }
}
