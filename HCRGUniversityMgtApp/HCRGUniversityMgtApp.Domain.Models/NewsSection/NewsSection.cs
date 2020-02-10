
namespace HCRGUniversityMgtApp.Domain.Models.NewsSectionModel
{
  public class NewsSection : Base.BaseColumn
    {
        public int NewsSectionID { get; set; }
        public string NewsSectionTitle { get; set; }
        public string NewsSectionType { get; set; }
        public bool flag { get; set; }
    }
}
