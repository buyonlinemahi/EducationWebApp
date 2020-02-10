using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel
{
    public class EducationModule1ViewModel
    {
        public List<EducationModule1> list_educationModule { get; set; }
        public PagedEducationModule pagedEducationModule { get; set; }
    }
}
