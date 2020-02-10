using HCRGUniversityApp.Domain.Models.SuggestCourse;
using HCRGUniversityApp.Domain.Models.Common;
using System.Collections.Generic;

using HCRGUniversityApp.Domain.Models.DiscountCouponModel;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;

namespace HCRGUniversityApp.Domain.ViewModels.SuggestCourseViewModel
{
    public class SuggestCourseViewModel
    {
        public SuggestCourse SuggestCourseResults { get; set; }
        public IEnumerable<State> StateResults { get; set; }
    }
}
