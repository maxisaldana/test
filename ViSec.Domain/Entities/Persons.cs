using FourPoints.FWK.Domain;

namespace ViSec.Domain.Entities
{
    public class Persons : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
