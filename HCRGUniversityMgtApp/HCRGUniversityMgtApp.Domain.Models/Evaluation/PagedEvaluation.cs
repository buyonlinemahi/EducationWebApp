using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EvaluationModel
{
    public class PagedEvaluation
    {
        public IEnumerable<Evaluation> Evaluations { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
