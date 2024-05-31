using Mashinin.DTOs.ModelDTOs;

namespace Mashinin.DTOs.MakeDTOs
{
    public class MakeGetDTO
    {
        public int Id { get; set; }
        public int TurboAzId { get; set; }
        public string Name { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }
    }
}
