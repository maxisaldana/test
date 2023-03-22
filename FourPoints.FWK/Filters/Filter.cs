using FourPoints.FWK.Domain;
using System.Linq.Expressions;

namespace FourPoints.CrossCutting.Filters
{
    public class Filter<T, Key> : IFilter<T> where T : BaseEntity<Key>
    {
        public Paginable? Pagination { get; set; }
        public ICollection<string>? Sort { get; set; }

        public virtual Expression<Func<T, bool>> GetFilters()
        {
            return x => true;
        }

        public Filter()
        {
            Pagination = new Paginable();
        }
    }
}
