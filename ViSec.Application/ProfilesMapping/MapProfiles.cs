using AutoMapper;
using ViSec.Application.Dtos;
using ViSec.Application.Filters;
using ViSec.Domain.Entities;

namespace ViSec.Application.ProfilesMapping
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<PersonDto, Persons>().ForMember(x => x.Id, act => act.Ignore());
            CreateMap<Persons, PersonDto>();

            CreateMap<PersonFilter, Persons>().ReverseMap();
        }
    }
}
