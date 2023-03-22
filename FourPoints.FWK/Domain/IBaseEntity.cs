namespace FourPoints.FWK.Domain
{
    public interface IBaseEntity<Key>
    {
        public Key Id { get; set; }
    }
}
