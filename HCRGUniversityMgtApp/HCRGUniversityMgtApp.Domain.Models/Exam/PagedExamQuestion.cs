using HCRGUniversityMgtApp.Domain.Models.PreTestModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.ExamModel
{
    public class PagedExamQuestion
    {
        public IEnumerable<ExamQuestion> ExamQuestions { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
