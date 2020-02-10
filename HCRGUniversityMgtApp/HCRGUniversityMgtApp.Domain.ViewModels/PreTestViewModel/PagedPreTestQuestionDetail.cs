using HCRGUniversityMgtApp.Domain.Models.PreTestModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.PreTestViewModel
{
    public class PagedPreTestQuestionDetail
    {
        public PagedPreTestQuestion pagedPreTestQuestion { get; set; }
        public PreTest preTest { get; set; }
    }
}
