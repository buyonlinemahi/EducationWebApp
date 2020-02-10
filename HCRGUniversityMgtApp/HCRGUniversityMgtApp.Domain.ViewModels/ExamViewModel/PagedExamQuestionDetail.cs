using HCRGUniversityMgtApp.Domain.Models.ExamModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.ExamViewModel
{
    public class PagedExamQuestionDetail
    {
        public PagedExamQuestion pagedExamQuestion { get; set; }
        public Exam Exam { get; set; }
    }
}
