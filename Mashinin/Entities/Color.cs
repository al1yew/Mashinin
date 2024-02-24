namespace Mashinin.Entities
{
    public class Color : BaseEntity
    {
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string HexCode { get; set; }

        public List<Transport> Transports { get; set; }

    }
}
