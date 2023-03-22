using FourPoints.FWK.Domain;
using FourPoints.CrossCutting.Filters;
using FourPoints.CrossCutting.Filters.Pagination;
using System.Linq.Expressions;

namespace FourPoints.FWK.Interfaces
{
    public interface IGenericRepository<Entity, Key> where Entity : BaseEntity<Key>
    {
        Task Add(Entity entity);
        Task AddRange(IEnumerable<Entity> entities);
        Task Remove(Entity entity);
        Task RemoveRange(IEnumerable<Entity> entities);
        Task Remove(Key Id);
        Task Update(Entity entity);
        Task UpdateRange(IEnumerable<Entity> entities);
        Task<PaginatedResult<Entity>> GetAll(Expression<Func<Entity, bool>>? expression, Filter<Entity, Key> filter, ICollection<string>? includes = null);
        Task<Entity?> GetOne(Key Id);
        Task SaveChangesAsync();
    }
}
