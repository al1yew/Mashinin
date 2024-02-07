namespace Mashinin.Entities
{
    public class NumberPlate : BaseEntity
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public bool IsForBargain { get; set; } //torq umesten ili net

        //price history
        //user
    }
}
