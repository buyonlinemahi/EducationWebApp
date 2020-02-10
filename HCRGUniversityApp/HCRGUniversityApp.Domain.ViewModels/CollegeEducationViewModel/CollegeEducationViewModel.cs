using HCRGUniversityApp.Domain.Models.CollegeEducationModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.CollegeEducationViewModel
{
    public class CollegeEducationViewModel
    {
        public IEnumerable<EducationDetail> EducationDetailResults { get; set; }
        public bool? ShowOnlineCourseOnly { get; set; }
        public int TotalCount { get; set; }
    }
}
