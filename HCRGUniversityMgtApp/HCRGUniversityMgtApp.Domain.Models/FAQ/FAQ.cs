namespace HCRGUniversityMgtApp.Domain.Models.FAQModel
{
    public class FAQ  :Base.BaseColumn
    {
        public int FAQID { get; set; }
        public int FAQCatID { get; set; }
        public string FAQues { get; set; }
        public string FAQAns { get; set; }
        public string FAQCategoryTitle { get; set; }
        public bool flag { get; set; }
        public string FaqCategoryName { get; set; }
        public string OrganizationName { get; set; }
    }
}
