using Mashinin.Entities;

namespace Mashinin.DTOs.TransportDTOs
{
    public class TransportImageGetDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsLowResolution { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
