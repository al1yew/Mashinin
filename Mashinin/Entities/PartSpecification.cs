namespace Mashinin.Entities
{
    public class PartSpecification : BaseEntity
    {
        public int PartCategoryId { get; set; }
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        //brand or subcategory

        public List<Part> Parts { get; set; }
    }
}
