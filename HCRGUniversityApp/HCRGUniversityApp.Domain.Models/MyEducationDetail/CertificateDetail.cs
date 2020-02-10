using System;

namespace HCRGUniversityApp.Domain.Models.CertificateDetail
{
    public class CertificateDetail
    {
        public string CourseName { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Name { get; set; }
        public string CertificateMessage { get; set; }
        public string AccreditorName { get; set; }
        public decimal? CredentialUnit { get; set; }
        public string CredentialProgram { get; set; }
        public string CertificatelogoPath { get; set; }
        public string CertificateSealedlogoPath { get; set; }
    }
}
