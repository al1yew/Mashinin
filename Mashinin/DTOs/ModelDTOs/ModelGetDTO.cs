using Mashinin.DTOs.MakeDTOs;

namespace Mashinin.DTOs.ModelDTOs
{
    public class ModelGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TurboAzId { get; set; }
        public string Class { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }

        public string Make { get; set; }
        public int MakeTurboAzId { get; set; }
        public int MakeId { get; set; }
    }
}
