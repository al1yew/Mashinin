using Mashinin.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mashinin.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<List<TEntity>> GetAllAsync(params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null && includes.Length > 0)
            {
                foreach (string inc in includes)
                {
                    query = query.Include(inc);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllByExAsync(Expression<Func<TEntity, bool>> ex, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(ex);

            if (includes != null && includes.Length > 0)
            {
                foreach (string inc in includes)
                {
                    query = query.Include(inc);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> ex, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(ex);

            if (includes != null && includes.Length > 0)
            {
                foreach (string inc in includes)
                {
                    query = query.Include(inc);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> DoesExistAsync(Expression<Func<TEntity, bool>> ex)
        {
            return await _context.Set<TEntity>().AnyAsync(ex);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
