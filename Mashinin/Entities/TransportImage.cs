namespace Mashinin.Entities
{
    public class TransportImage 
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsLowResolution { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }

        public Transport Transport { get; set; }
        public int TransportId { get; set; }
    }
}
