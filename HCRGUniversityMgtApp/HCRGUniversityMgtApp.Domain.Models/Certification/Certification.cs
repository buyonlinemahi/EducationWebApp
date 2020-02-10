
namespace HCRGUniversityMgtApp.Domain.Models.Certification
{
    public class Certification : Base.BaseColumn
    {
       public int CertificationID { get; set; }
       public string CertificationContent { get; set; }
       public string DescriptionShort { get; set; }
       public string OrganizationName { get; set; }
       public bool flag { get; set; }

    }
}
