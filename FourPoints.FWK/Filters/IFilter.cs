using System.Linq.Expressions;

namespace FourPoints.CrossCutting.Filters
{
    public interface IFilter<T>
    {
        public Paginable? Pagination { get; set; }
        public ICollection<string>? Sort { get; set; }
        public Expression<Func<T, bool>> GetFilters();
    }
}
