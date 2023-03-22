namespace FourPoints.CrossCutting.Filters.Pagination
{
    public class PaginatedResult<T>
    {
        public PaginationMeta Meta { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
