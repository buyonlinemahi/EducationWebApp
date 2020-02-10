using HCRGUniversityApp.Domain.Models.EducationModuleFile;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.EducationModuleFileViewModel
{
    public class EducationModuleFileViewModel
    {
        public IEnumerable<EducationModuleFile> EducationModuleFileResults { get; set; }
    }
}
