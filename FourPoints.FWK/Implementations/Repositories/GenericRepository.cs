using FourPoints.CrossCutting.Filters;
using System.Linq.Expressions;
using FourPoints.CrossCutting.Filters.Pagination;
using FourPoints.FWK.Interfaces;
using FourPoints.FWK.Domain;
using FourPoints.FWK.Context;
using Microsoft.EntityFrameworkCore;
using FourPoints.FWK.Exceptions;
using System.Linq.Dynamic.Core;

namespace FourPoints.FWK.Implementations.Repositories
{
    public class GenericRepository<Entity, Key> : IGenericRepository<Entity, Key> where Entity : BaseEntity<Key>
    {
        protected readonly BaseContext<Key> _ctx;
        protected readonly DbSet<Entity> _dbSet;

        public GenericRepository(BaseContext<Key> ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<Entity>();
        }

        public async Task Add(Entity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Entity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<Entity?> GetOne(Key Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task Remove(Entity entity)
        {
            await FindOrFail(entity.Id);
            _dbSet.Remove(entity);
        }

        public async Task Remove(Key Id)
        {
            var entity = await FindOrFail(Id);
            await Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Entity> entities)
        {
            await Task.Yield();
            _dbSet.RemoveRange(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task Update(Entity entity)
        {
            await Task.Yield();
            _ctx.Update(entity);
        }

        public async Task UpdateRange(IEnumerable<Entity> entities)
        {
            await Task.Yield();
            _ctx.UpdateRange(entities);
        }

        public async Task<PaginatedResult<Entity>> GetAll(Expression<Func<Entity, bool>>? expression, Filter<Entity, Key> filter, ICollection<string>? includes = null)
        {
            var (filtered, count) = this.FindAndCountAll(expression, filter, includes);
            var entities = await filtered.AsNoTracking().ToListAsync();
            return new PaginatedResult<Entity>
            {
                Data = entities,
                Meta = new PaginationMeta
                {
                    Limit = filter.Pagination.Limit,
                    Page = filter.Pagination.Page,
                    Count = count,
                }
            };
        }

        private async Task<Entity> FindOrFail(Key Id)
        {
            var entity = await GetOne(Id);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            return entity;
        }

        private (IQueryable<Entity>, int) FindAndCountAll(Expression<Func<Entity, bool>>? expression, Filter<Entity, Key> filter, ICollection<string>? includes = null)
        {
            var pageSize = filter.Pagination.Limit.Value;
            var page = filter.Pagination.Page.Value;
            var skip = pageSize * (page - 1);
            expression = expression ?? (x => true);
            var filtered = Preload(includes).Where(expression);
            var count = filtered.Count();
            if (filter.Sort != null && filter.Sort.Any())
            {
                var orderBy = string.Join(",", filter.Sort);
                filtered = filtered.OrderBy(orderBy);
            }
            var entities = filtered.Skip(skip).Take(pageSize).AsQueryable();
            return (entities, count);
        }

        private IQueryable<Entity> Preload(ICollection<string>? includes = null)
        {
            var entity = _dbSet.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                    entity = entity.Include(include);
            }
            return entity;
        }
    }
}
