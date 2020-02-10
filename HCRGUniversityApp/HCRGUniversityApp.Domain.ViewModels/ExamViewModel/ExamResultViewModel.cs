using HCRGUniversityApp.Domain.Models.ExamModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.ExamViewModel
{
    public class ExamResultViewModel
    {
        public decimal TotalPercentage { get; set; }
        public string encryptedTotalPercentage { get; set; }
        public int FinalExamAttemptByUser { get; set; }
        public IEnumerable<ExamResult> ExamResult { get; set; }
    }
}
