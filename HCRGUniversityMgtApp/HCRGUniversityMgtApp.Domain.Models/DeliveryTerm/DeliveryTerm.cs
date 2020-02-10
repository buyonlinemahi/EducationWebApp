
namespace HCRGUniversityMgtApp.Domain.Models.DeliveryTerm
{
    public class DeliveryTerm : Base.BaseColumn
    {
        public int DeliveryTermID { get; set; }
        public string DeliveryTermContents { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }

    }
}
