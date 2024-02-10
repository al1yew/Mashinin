namespace Mashinin.DTOs.StatisticsDTOs
{
    public class GetStatisticsDTO
    {
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int BodyType { get; set; }
        public int EngineVolume { get; set; }
        public int FuelType { get; set; }
        public double Odometer { get; set; }
        public int TransmissionType { get; set; }
        public int Year { get; set; }
    }
}
