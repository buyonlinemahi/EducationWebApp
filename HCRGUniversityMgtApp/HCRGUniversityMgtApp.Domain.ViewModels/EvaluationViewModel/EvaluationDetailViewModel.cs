using HCRGUniversityMgtApp.Domain.Models.EvaluationModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EvaluationViewModel
{
  public class EvaluationDetailViewModel
    {
        public Evaluation Evaluation { get; set; }
        public EvaluationQuestion EvaluationQuestion { get; set; }
      public IEnumerable<Evaluation> EvaluationResults { get; set; }
    }
}
