namespace Mashinin.DTOs.CityDTOs
{
    public class CityGetDTO
    {
        public int Id { get; set; }
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public int Index { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string CreatedAt { get; set; }
    }
}
