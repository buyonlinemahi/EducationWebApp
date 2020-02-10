using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EducationModel
{
    public class PagedEducationModule
    {
        public IEnumerable<EducationModule> EducationModules { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
