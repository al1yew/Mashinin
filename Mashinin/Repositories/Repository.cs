using Mashinin.IRepositories;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
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
                    query = query.Include(inc);
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllByExAsync(
            Expression<Func<TEntity, bool>> filters,
            params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(filters);

            if (includes != null && includes.Length > 0)
            {
                foreach (string inc in includes)
                    query = query.Include(inc);
            }

            return await query.ToListAsync();
        }

        public async Task<List<TResult>> GetSelectedByExAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filters = null,
            params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filters != null)
                query = query.Where(filters);

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.Select(selector).ToListAsync();
        }

        public async Task<List<TResult>> GetFilteredAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filters != null)
                query = query.Where(filters);

            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.Select(selector).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filters, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(filters);

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

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
