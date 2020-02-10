
namespace HCRGUniversityApp.Domain.Models.EducationModel
{
    public class EducationCredential
    {
        public int CredentialID { get; set; }
        public decimal? CredentialUnit { get; set; }
        public string CredentialProgram { get; set; }
        public string CredentialType { get; set; }
        public int AccreditorID { get; set; }
        public string CertificateMessage { get; set; }
        public int EducationID { get; set; }
    }
}
