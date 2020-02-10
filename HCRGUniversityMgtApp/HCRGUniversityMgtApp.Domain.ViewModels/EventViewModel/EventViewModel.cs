using HCRGUniversityMgtApp.Domain.Models.EventModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EventViewModel
{
    public class EventViewModel
    {
        public IEnumerable<Event> EventResults { get; set; }
    }
}
