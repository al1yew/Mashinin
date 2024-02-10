namespace Mashinin.Entities
{
    public class Report : BaseEntity
    {
        public string Vin { get; set; }
        public string FileName { get; set; }
        public int Type { get; set; }
        //sozdat enum dla tipov vsex reportov kotorie u mena budut

        //user relation
    }
}
