using HCRGUniversityApp.Domain.Models.ExamModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.ExamViewModel
{
   public class PreTestQuestionDetailViewModel
    {
       public IEnumerable<PreTestQuestionDetail> PreTestQuestionDetailResults { get; set; }
       public IEnumerable<EvaluationQuestionDetail> EvaluationQuestionDetailResults { get; set; }
       public IEnumerable<EvaluationAnswer> EvaluationAnswers { get; set; }
       public int PreTestAttemptByUser { get; set; } 
    }
}
