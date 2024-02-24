namespace Mashinin.Entities
{
    public class Report : BaseEntity
    {
        public string Vin { get; set; }
        public string FileName { get; set; }
        public int Type { get; set; } //enum
        

        //user relation
    }
}
