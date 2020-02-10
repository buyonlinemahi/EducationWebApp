using HCRGUniversityApp.Domain.Models.CollegeModel;
using HCRGUniversityApp.Domain.Models.EducationModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.EducationViewModel
{
    public class EducationCollegeViewModel
    {
        public IEnumerable<Education> EducationResults { get; set; }
        public IEnumerable<College> CollegeResults { get; set; }

    }

   
}
