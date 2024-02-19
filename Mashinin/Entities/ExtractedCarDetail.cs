namespace Mashinin.Entities
{
    public class ExtractedCarDetail
    {
        public int Id { get; set; }
        public Nullable<DateTime> PostCreatedAt { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        
        
        public double Price { get; set; }
        public string Currency { get; set; }
        public int EngineVolume { get; set; }
        public int Year { get; set; }
        public int Odometer { get; set; }
        public bool IsNew { get; set; }
        public string Link { get; set; }


        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int TurboAzMakeId { get; set; }
        public int TurboAzModelId { get; set; }
    }
}
