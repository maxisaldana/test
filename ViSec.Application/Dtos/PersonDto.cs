using FourPoints.FWK.Domain;
using System.Text.Json.Serialization;

namespace ViSec.Application.Dtos
{
    public class PersonDto : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
