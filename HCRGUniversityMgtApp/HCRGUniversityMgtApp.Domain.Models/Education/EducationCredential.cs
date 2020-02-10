
namespace HCRGUniversityMgtApp.Domain.Models.EducationCredential
{
  public class EducationCredential
    {
        public int CredentialID { get; set; }
        public int EducationID { get; set; }
        public string CredentialUnit { get; set; }
        public string CredentialType { get; set; }
        public string CredentialProgram { get; set; }
        public string CertificateMessage { get; set; }
        public string AccreditorName { get; set; }
        public int AccreditorID { get; set; }
        public bool IsActive { get; set; }      
        public bool flag { get; set; }
     
    }
}
