using HCRGUniversityMgtApp.Domain.Models.PreTestModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.PreTestViewModel
{
    public class PreTestDetailViewModel
    {
        public PreTest preTest {get;set;}
        public PreTestQuestion preTestQuestion { get; set; }
        public IEnumerable<PreTest> PreTestResults { get; set; }
    }
}
