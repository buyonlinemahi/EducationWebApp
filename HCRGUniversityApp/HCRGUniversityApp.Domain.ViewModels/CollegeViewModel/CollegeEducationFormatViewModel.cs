using HCRGUniversityApp.Domain.Models.CollegeModel;
using HCRGUniversityApp.Domain.Models.EducationFormatModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.CollegeViewModel
{
    public class CollegeEducationFormatViewModel
    {
        public IEnumerable<College> CollegeResults { get; set; }
        public IEnumerable<EducationFormat> EducationFormatResults { get; set; }
    }
}
