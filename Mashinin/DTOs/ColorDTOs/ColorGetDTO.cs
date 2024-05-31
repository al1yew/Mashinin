namespace Mashinin.DTOs.ColorDTOs
{
    public class ColorGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexCode { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }
    }
}
