using HCRGUniversityApp.Domain.Models.EducationModel;
using HCRGUniversityApp.Domain.Models.Event;
using HCRGUniversityApp.Domain.Models.NewsModel;
using HCRGUniversityApp.Domain.Models.OrganizationModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.HomeViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Education> EducationResults { get; set; }
        public string HomePageContent { get; set; }
        public IEnumerable<News> NewsResults { get; set; }
        public IEnumerable<EventDetail> EventDetails { get; set; }
    }
}
