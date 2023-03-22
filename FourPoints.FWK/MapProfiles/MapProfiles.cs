using AutoMapper;
using FourPoints.CrossCutting.Filters.Pagination;

namespace FourPoints.FWK.MapProfiles
{
    public class MapProfiles : Profile
    {
        public MapProfiles() {
            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
        }
    }
}
