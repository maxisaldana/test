using AutoMapper;
using FourPoints.FWK.Domain;
using FourPoints.FWK.Exceptions;
using FourPoints.FWK.Interfaces;
using FourPoints.CrossCutting.Filters;
using FourPoints.CrossCutting.Filters.Pagination;

namespace FourPoints.FWK.Implementations.Services
{
    public class BaseService<Entity, Key, Filters> : IBaseService<Entity, Key, Filters>
        where Entity  : BaseEntity<Key>
        where Filters : Filter<Entity, Key>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Entity, Key> _repository;

        public BaseService(IMapper mapper, IGenericRepository<Entity, Key> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<DtoOut> Create<DtoOut, DtoIn>(DtoIn dto)
        {
            var entity = _mapper.Map<DtoIn, Entity>(dto);
            await _repository.Add(entity);
            await _repository.SaveChangesAsync();
            var model = _mapper.Map<Entity, DtoOut>(entity);
            return model;
        }

        public virtual async Task<PaginatedResult<DtoOut>> GetAll<DtoOut>(Filters filter, ICollection<string>? includes = null)
        {
            var entities = await _repository.GetAll(filter.GetFilters(), filter, includes);
            var model = _mapper.Map<PaginatedResult<DtoOut>>(entities);
            return model;
        }
        public virtual async Task<DtoOut> GetOne<DtoOut>(Key key)
        {
            var entity = await _repository.GetOne(key);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var model = _mapper.Map<Entity, DtoOut>(entity);
            return model;
        }

        public virtual async Task Remove(Key key)
        {
            await _repository.Remove(key);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task Remove(Entity entity)
        {
            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task<DtoOut> Update<DtoOut, DtoIn>(Key id, DtoIn dto) where DtoIn : IBaseEntity<Key>
        {
            var payload = _mapper.Map<DtoIn, Entity>(dto);
            payload.Id = id;
            await _repository.Update(payload);
            await _repository.SaveChangesAsync();
            var model = _mapper.Map<Entity, DtoOut>(payload);
            return model;
        }
    }
}
