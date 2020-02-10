
namespace HCRGUniversityMgtApp.Domain.Models.AccreditationModel
{
   public class Accreditation : Base.BaseColumn
    {
        public int AccreditationID { get; set; }
        public string AccreditationContent { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }
        public string OrganizationName { get; set; }
    }
}
