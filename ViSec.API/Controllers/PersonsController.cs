using Microsoft.AspNetCore.Mvc;
using ViSec.Application.Dtos;
using ViSec.Application.Filters;
using ViSec.Domain.Entities;
using FourPoints.FWK.Implementations.Controllers;
using FourPoints.FWK.Interfaces;

namespace ViSec.API.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : BaseController<Persons, long, PersonDto, PersonFilter>
    {
        public PersonsController(IBaseService<Persons, long, PersonFilter> service) : base(service)
        {
        }
    }
}
