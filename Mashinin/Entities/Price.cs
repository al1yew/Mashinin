namespace Mashinin.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int Currency { get; set; } //enum
        public DateTime CreatedAt { get; set; }
        public Transport Transport { get; set; }
        public int TransportId { get; set; }
    }
}
