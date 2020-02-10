using System;

namespace HCRGUniversityMgtApp.Domain.Models.SuggestCourse
{
    public class SuggestCourse : Base.BaseColumn
    {
        public int SuggestCourseID { get; set; }
        public string SCMyOccupation { get; set; }
        public int SCStateID { get; set; }       
        public string SCName { get; set; }
        public string SCPhone { get; set; }
        public string SCEmail { get; set; }
        public string SCPossibleTitle { get; set; }
        public string SCBriefAgendaOutline { get; set; }
        public string SCAudience { get; set; }
        public bool? SCSingleDaySeminarCourse { get; set; }
        public bool? SCOndemandLiveWebinarCourse { get; set; }
        public bool? SCInterestedInstructor { get; set; }
        public DateTime? SCCreatedDate { get; set; }
        public string SCStateName { get; set; }
        public string OrganizationName { get; set; }
    }
}
