namespace Mashinin.Entities
{
    public class PartImage : BaseEntity
    {
        public string Path { get; set; }
        public Part Part { get; set; }
        public Part PartId { get; set; }
    }
}
