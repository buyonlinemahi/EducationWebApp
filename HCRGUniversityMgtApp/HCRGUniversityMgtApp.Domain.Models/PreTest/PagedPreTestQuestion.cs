using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.PreTestModel
{
    public class PagedPreTestQuestion
    {
        public IEnumerable<PreTestQuestion> PreTestQuestions { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
