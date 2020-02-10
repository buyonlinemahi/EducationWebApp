using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.Certification;
namespace HCRGUniversityMgtApp.Domain.ViewModels.CertificationViewModel
{
   public  class CertificationViewModel
    {
       public IEnumerable<Certification> CertificationResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
