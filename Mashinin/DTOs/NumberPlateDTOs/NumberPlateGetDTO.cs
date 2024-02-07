using Mashinin.DTOs.ModelDTOs;

namespace Mashinin.DTOs.NumberPlateDTOs
{
    public class NumberPlateGetDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }
    }
}
