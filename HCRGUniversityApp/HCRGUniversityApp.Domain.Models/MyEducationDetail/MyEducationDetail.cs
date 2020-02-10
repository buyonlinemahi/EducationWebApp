using HCRGUniversityApp.Domain.Models.ExamModel;
using System;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.Models.MyEducationDetailModel
{
    public class MyEducationDetail
    {
        public int MEID { get; set; }
        public string EncryptedMEID { get; set; }
        public string EncryptedEducationID { get; set; }
        public int UserID { get; set; }
        public int EducationID { get; set; }
        public bool Completed { get; set; }
        public string CourseName { get; set; }
        public int TotalModuleCompleted { get; set; }
        public int TotalModule { get; set; }
        public int percentCompleted { get; set; }
        public string CourseFile { get; set; }
        public int PreTestAttemptByUser { get; set; }
        public int FinalExamAttemptByUser { get; set; }
        public bool? IsPassed { get; set; }
        public bool IsExamPass { get; set; }
        public bool Expired { get; set; }
        public bool IsPreTestAvailable { get; set; }
        public bool IsExamAvailable { get; set; }
        public bool IsEvalutionAvailable { get; set; }
        public IEnumerable<ExamResult>  ExamResult { get; set; }
        public DateTime? CourseCompletedDate { get; set; }
        public bool? CertificatePrinted { get; set; }
        public string CertificateURL { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int DaysLeft { get; set; }
        public string PageUrl { get; set; }
        public string LogErrorMsg { get; set; }
        public bool AllowRevisit { get; set; }
        public bool? IsCertificationTitleOption { get; set; }
        public int CertificationTitleOptionID { get; set; }
        public int PlanID { get; set; }        
    }
}
