using HCRGUniversityApp.Domain.Models.EducationModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.EducationTypeAvailableViewModel
{
    public class EducationTypesAvailableViewModel
    {
        public IEnumerable<EducationTypesAvailable> EducationTypesAvailableResults { get; set; }
        public EducationDetailPageWithEducation EducationDetailPageResults { get; set; }
        public IEnumerable<EducationCredential> EducationCredentialResults { get; set; }
        public Education Education { get; set; }
    }
}
