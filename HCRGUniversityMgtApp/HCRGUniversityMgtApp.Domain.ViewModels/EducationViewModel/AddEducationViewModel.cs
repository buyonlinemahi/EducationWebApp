using HCRGUniversityMgtApp.Domain.Models.AccreditorModel;
using HCRGUniversityMgtApp.Domain.Models.CollegeEducation;
using HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel;
using HCRGUniversityMgtApp.Domain.Models.EducationCredential;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatAvailableModel;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel;
using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using HCRGUniversityMgtApp.Domain.Models.EducationProfessionModel;
using HCRGUniversityMgtApp.Domain.Models.EvaluationModel;
using HCRGUniversityMgtApp.Domain.Models.ExamModel;
using HCRGUniversityMgtApp.Domain.Models.PreTestModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel
{
    public class AddEducationViewModel
    {
        public Education Education { get; set; }
        public IEnumerable<CollegeEducationSearch> CollegeEducation { get; set; }
        public IEnumerable<EducationProfession> ProfessionEducation { get; set; }
        public IEnumerable<CollegeEducation> CollegeEducationDeleted { get; set; }
        public IEnumerable<EducationProfession> ProfessionEducationDeleted { get; set; }        
        public IEnumerable<EducationFormatDetail> EducationFormatAvailable { get; set; }
        public IEnumerable<EducationFormatAvailable> DeletedEducationFormatAvailable { get; set; }
        public IEnumerable<EducationModule> EducationModuleResults { get; set; }
        public PagedEducationModule EducationModules { get; set; }
        public EducationDetailPage EducationDetailPage { get; set; }
        public EducationPreTestQuestion EducationPreTestQuestion { get; set; }
        public EducationExamQuestion EducationExamQuestion { get; set; }
        public EducationEvaluation EducationEvaluation { get; set; }
        public IEnumerable<Evaluation> EvaluationResults { get; set; }
        public IEnumerable<Exam> ExamResults { get; set; }
        public IEnumerable<PreTest> PreTestResults { get; set; }
        public IEnumerable<EducationCredential> EducationCredentialResults { get; set; }
        public IEnumerable<Accreditor> AccreditorResults { get; set; }
        public int CoursePreTestID { get; set; }
        public int CourseExamID { get; set; }
        public int CourseEvaluationID { get; set; }
        public string CourseName { get; set; }
        public EducationModuleFile EducationModuleFile { get; set; }
    }
}