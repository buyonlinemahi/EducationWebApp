using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel
{
    public class EducationModuleViewModel
    {
        public IEnumerable<EducationModule> EducationModuleResults { get; set; }
       
    }
}
