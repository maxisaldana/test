using FourPoints.FWK.Constants;

namespace FourPoints.CrossCutting.Filters
{
    public class Paginable
    {

        private int? _page;
        private int? _limit;

        public int? Page
        {
            get
            {
                return _page ?? 1;
            }
            set
            {
                _page = Math.Max(1, value ?? 1);
            }
        }

        public int? Limit
        {
            get
            {
                return _limit ?? FourPoints.FWK.Constants.Pagination.DEFAULT_RESULTS_PER_PAGE;
            }
            set
            {
                _limit = Math.Min(FourPoints.FWK.Constants.Pagination.MAX_RESULTS_PER_PAGE, value ?? FourPoints.FWK.Constants.Pagination.DEFAULT_RESULTS_PER_PAGE);
                if (_limit == 0)
                {
                    _limit = FourPoints.FWK.Constants.Pagination.DEFAULT_RESULTS_PER_PAGE;
                }
            }
        }
    }
}
