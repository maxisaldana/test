using FourPoints.FWK.Domain;
using FourPoints.CrossCutting.Filters;
using FourPoints.CrossCutting.Filters.Pagination;

namespace FourPoints.FWK.Interfaces
{
    public interface IBaseService<Entity, Key, Filters>
        where Filters : Filter<Entity, Key>
        where Entity : BaseEntity<Key>
    {
        public Task<DtoOut> Create<DtoOut, DtoIn>(DtoIn dto);
        public Task Remove(Key key);
        public Task Remove(Entity entity);
        public Task<DtoOut> Update<DtoOut, DtoIn>(Key id, DtoIn dto) where DtoIn : IBaseEntity<Key>;
        public Task<DtoOut> GetOne<DtoOut>(Key key);
        public Task<PaginatedResult<DtoOut>> GetAll<DtoOut>(Filters filter, ICollection<string>? includes = null);
    }
}
