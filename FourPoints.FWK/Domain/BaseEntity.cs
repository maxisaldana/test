using System.ComponentModel.DataAnnotations;

namespace FourPoints.FWK.Domain
{
    public class BaseEntity<Key> : IBaseEntity<Key>
    {
        [Key]
        public Key Id { get; set; }
    }
}
