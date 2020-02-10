using HCRGUniversityMgtApp.Domain.Models.Education;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EducationModel
{
    public class PagedEducation
    {
        public IEnumerable<EducationSearchResult> Educations { get; set; }
        public IEnumerable<Education> _educations { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
