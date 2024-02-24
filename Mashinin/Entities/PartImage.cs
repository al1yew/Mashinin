namespace Mashinin.Entities
{
    public class PartImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsLowResolution { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }

        public Part Part { get; set; }
        public int PartId { get; set; }
    }
}
