using HCRGUniversityApp.Domain.Models.CertificationTitleOption;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.CertificationTitleOptionViewModel
{
    public class CertificationTitleOptionViewModel
    {
        public IEnumerable<CertificationTitleOption> CertificationTitleOptionResults { get; set; }
        public int TotalCount { get; set; }
    }
}
