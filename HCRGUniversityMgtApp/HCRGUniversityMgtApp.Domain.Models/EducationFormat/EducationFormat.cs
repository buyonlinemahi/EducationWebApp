
namespace HCRGUniversityMgtApp.Domain.Models.EducationFormatModel
{
    public class EducationFormat :Base.BaseColumn
    {
        public int EducationFormatID { get; set; }
        public string EducationFormatType { get; set; }
        public bool flag { get; set; }
        public string OrganizationName { get; set; }
    }
}

