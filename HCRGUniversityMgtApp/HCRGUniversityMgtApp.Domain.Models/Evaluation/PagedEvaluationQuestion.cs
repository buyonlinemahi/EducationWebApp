using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EvaluationModel
{
    public class PagedEvaluationQuestion
    {
        public IEnumerable<EvaluationQuestion> EvaluationQuestions { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
