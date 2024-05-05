using Newtonsoft.Json;

namespace Mashinin.DTOs.CityDTOs
{
    public class CityGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)] we can use that, but it is better to add more DTOs
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
