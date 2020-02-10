using HCRGUniversityMgtApp.Domain.Models.ProfessionModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.ProfessionViewModel
{
    public class ProfessionViewModel
    {
        public IEnumerable<Profession> ProfessionResults { get; set; }
    }
}
