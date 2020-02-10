using HCRGUniversityMgtApp.Domain.Models.LogSessionModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.LogSessionViewModel
{
    public class LogSessionViewModel
    {
        public IEnumerable<LogSessionDetail> LogSessionResults { get; set; }
    }
}
