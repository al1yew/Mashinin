using Mashinin.Entities;

namespace Mashinin.DTOs.TransportDTOs
{
    public class PriceGetDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int Currency { get; set; } //enum
        public DateTime CreatedAt { get; set; }
    }
}
