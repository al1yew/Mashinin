namespace Mashinin.DTOs.ExtractedCarDetailDTOs
{
    public class ExtractedCarDetailGetDTO
    {
        public int Id { get; set; }
        public string PostCreatedAt { get; set; }
        public string CreatedAt { get; set; }


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
