using HCRGUniversityMgtApp.Domain.Models.EvaluationModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EvaluationViewModel
{
    public class PagedEvaluationQuestionDetail
    {
        public PagedEvaluationQuestion pagedEvaluationQuestion { get; set; }
        public Evaluation Evaluation { get; set; }
    }
}
