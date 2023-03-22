using ViSec.Domain.Entities;
using FourPoints.CrossCutting.Filters;
using System.Linq.Expressions;

namespace ViSec.Application.Filters
{
    public class PersonFilter : Filter<Persons, long>
    {
        public string? Name { get; set; }
        public override Expression<Func<Persons, bool>> GetFilters()
        {
            return x => string.IsNullOrEmpty(Name) || x.Name.ToLower().Contains(Name.ToLower());
        }
    }
}
