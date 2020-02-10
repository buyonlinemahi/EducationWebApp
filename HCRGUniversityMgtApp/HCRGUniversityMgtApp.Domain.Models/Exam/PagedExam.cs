using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.ExamModel
{
    public class PagedExam
    {
        public IEnumerable<Exam> Exams { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
