namespace FourPoints.FWK.Domain
{
    public interface ISoftDelete
    {
        public DateTime? DeletedAt { get; set; }
    }
}
