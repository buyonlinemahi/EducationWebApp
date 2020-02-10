using HCRGUniversityApp.Domain.Models.ExamModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.ExamViewModel
{
    public class ExamQuestionDetailViewModel
    {
        public IEnumerable<ExamQuestionDetail> ExamQuestionDetailResults { get; set; }
        public IEnumerable<EvaluationQuestionDetail> EvaluationQuestionDetailResults { get; set; }
        public IEnumerable<EvaluationAnswer> EvaluationAnswers { get; set; }
        public int FinalExamAttemptByUser { get; set; }
        public IEnumerable<ExamResult> ExamResult { get; set; }
        public int UserID { get; set; }
        public int MEID { get; set; }
        public string SessionTimeout{ get; set; }
    }
}