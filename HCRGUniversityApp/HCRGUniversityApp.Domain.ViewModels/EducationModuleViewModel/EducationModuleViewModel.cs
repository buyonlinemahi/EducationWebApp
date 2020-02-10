using HCRGUniversityApp.Domain.Models.EducationModuleModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.EducationModuleViewModel
{
   public class EducationModuleViewModel
    {
       public IEnumerable<EducationModule> EducationModuleResults { get; set; }
    }
}
