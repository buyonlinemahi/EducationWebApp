using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.TrainingAndSeminar;

namespace HCRGUniversityMgtApp.Domain.ViewModels.TrainingAndSeminarViewModel
{
    public class TrainingSeminarViewModel
    {
        public IEnumerable<TrainingAndSeminar> TrainingAndSeminarResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
