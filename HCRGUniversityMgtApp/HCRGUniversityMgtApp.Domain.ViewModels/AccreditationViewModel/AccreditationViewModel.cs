
using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.AccreditationModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.AccreditationViewModel
{
    public class AccreditationViewModel
    {
        public IEnumerable<Accreditation> AccreditationResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
