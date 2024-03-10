namespace Mashinin.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public double? Value { get; set; } //for sale or for rent
        public int Currency { get; set; } //enum
        public DateTime CreatedAt { get; set; }


        //esli mashina privoznaya, ukazivat eto vse nado useru dla ego je polzi - esli forOrder
        public double? WinPriceMin { get; set; } //mashinin udush qiymeti OT
        public double? WinPriceMax { get; set; } //mashinin udush qiymeti DO
        public double? TransportationPrice { get; set; }//dashinma xercleri

   
        public Transport Transport { get; set; }
        public int TransportId { get; set; }
    }
}
