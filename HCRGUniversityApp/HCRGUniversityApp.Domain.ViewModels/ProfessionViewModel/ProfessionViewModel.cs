using HCRGUniversityApp.Domain.Models.ProfessionModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.ProfessionViewModel
{
    public class ProfessionViewModel
    {
        public IEnumerable<Profession> ProfessionResults { get; set; }
    }
}
