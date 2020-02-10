using HCRGUniversityApp.Domain.Models.EducationModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.Models.Education
{
  public class PagedEducationNewsSearch
    {
        public int TotalCount { get; set; }
        public IEnumerable<EducationNewsSearch> educationNewsSearch { get; set; }

    }
}
