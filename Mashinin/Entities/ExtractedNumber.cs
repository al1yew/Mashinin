namespace Mashinin.Entities
{
    public class ExtractedNumber
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int TurboAzMakeId { get; set; }
        public int TurboAzModelId { get; set; }
        public string PhoneNumber { get; set; }
        public string Link { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }

    }
}
