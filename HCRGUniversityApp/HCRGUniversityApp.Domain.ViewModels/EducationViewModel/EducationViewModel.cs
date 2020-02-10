using HCRGUniversityApp.Domain.Models.EducationModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.EducationViewModel
{
    public class EducationViewModel
    {
        public IEnumerable<Education> EducationResults { get; set; }
    }
}
