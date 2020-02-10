using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.SuggestCourse
{
    public class SuggestCourse
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
        public int? OrganizationID { get; set; }
    }
}
