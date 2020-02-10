
namespace HCRGUniversityMgtApp.Domain.Models.AccreditorModel
{
    public class Accreditor : Base.BaseColumn
    {
        public int AccreditorID { get; set; }
        public string AccreditorName { get; set; }
        public bool IsActive { get; set; }
        public bool flag { get; set; }
        public string OrganizationName { get; set; }
    }
}
