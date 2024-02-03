using System.Linq.Expressions;

namespace Mashinin.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<List<TEntity>> GetAllAsync(params string[] includes);
        Task<List<TEntity>> GetAllByExAsync(Expression<Func<TEntity, bool>> ex, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> ex, params string[] includes);
        void Remove(TEntity entity);
        Task<bool> DoesExistAsync(Expression<Func<TEntity, bool>> ex);
        void UpdateAsync(TEntity entity);
    }
}
