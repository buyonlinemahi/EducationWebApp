
namespace HCRGUniversityMgtApp.Domain.Models.NewsLetterModel
{
    public class NewsLetter : Base.BaseColumn
    {
        public int NewsLetterID { get; set; }
        public string NewsLetterContent { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }
        public string OrganizationName { get; set; }
    }
}
