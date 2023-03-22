using Microsoft.AspNetCore.Mvc;
using FourPoints.FWK.Domain;
using FourPoints.FWK.Interfaces;
using FourPoints.CrossCutting.Filters;
using FourPoints.CrossCutting.Filters.Pagination;

namespace FourPoints.FWK.Implementations.Controllers
{
    [ApiController]

    public class BaseController<Entity, Key, Dto, Filters> : ControllerBase
         where Entity : BaseEntity<Key>
         where Filters : Filter<Entity, Key>
         where Dto : IBaseEntity<Key>
    {
        private readonly IBaseService<Entity, Key, Filters> _service;

        public BaseController(IBaseService<Entity, Key, Filters> service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<Dto>> Create(Dto dto)
        {
            var entity = await _service.Create<Dto, Dto>(dto);
            return Created(nameof(GetOne), entity);
        }
        [HttpGet]
        public async Task<ActionResult<Dto>> GetAll([FromQuery] Filters filters)
        {
            return Ok(await _service.GetAll<Dto>(filters));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Dto>> GetOne(Key id)
        {
            return Ok(await _service.GetOne<Dto>(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(Key id)
        {
            await _service.Remove(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Dto>> Update(Key id, Dto dto)
        {
            return Ok(await _service.Update<Dto, Dto>(id, dto));
        }
    }
}
