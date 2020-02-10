using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationModuleFileViewModel
{
    public class EducationModuleFileViewModel
    {
        public string CourseName { get; set; }
        public PagedEducationModule EducationModules { get; set; }
        public EducationModuleFile educationModuleFile { get; set; }
        public IEnumerable<EducationModuleFile> EducationModuleFileResults { get; set; }
    }
}
