using HCRGUniversityApp.Domain.Models.ExamModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.ExamViewModel
{
   public class EvaluationQuestionDetailViewModel
    {
        public IEnumerable<EvaluationQuestionDetail> EvaluationQuestionDetailResults { get; set; }
        public IEnumerable<EvaluationAnswer> EvaluationAnswers { get; set; }
    }
}
