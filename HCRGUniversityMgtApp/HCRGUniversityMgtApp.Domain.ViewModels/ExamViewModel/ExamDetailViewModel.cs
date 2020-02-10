using HCRGUniversityMgtApp.Domain.Models.ExamModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.ExamViewModel
{
    public class ExamDetailViewModel
    {
        public Exam Exam {get;set;}
        public ExamQuestion ExamQuestion { get; set; }
        public IEnumerable<Exam> ExamResults { get; set; }
    }
}
