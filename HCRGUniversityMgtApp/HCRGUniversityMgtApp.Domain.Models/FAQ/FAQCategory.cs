namespace HCRGUniversityMgtApp.Domain.Models.FAQModel
{
    public class FAQCategory  
    {
        public int FAQCatID { get; set; }
        public string FAQCategoryTitle { get; set; }
        public bool flag { get; set; }
        public int OrganizationID { get; set; }
    }
}
