using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel
{
    public class EducationViewModel
    {
        public IEnumerable<Education> EducationResults { get; set; }
    }
}
