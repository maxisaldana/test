namespace FourPoints.FWK.Domain
{
    public interface IAuditable<Key>
    {
        public Key? CreatedBy { get; set; }
        public Key? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
