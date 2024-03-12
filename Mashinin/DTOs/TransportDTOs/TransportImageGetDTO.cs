using Mashinin.Entities;

namespace Mashinin.DTOs.TransportDTOs
{
    public class TransportImageGetDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
    }
}
