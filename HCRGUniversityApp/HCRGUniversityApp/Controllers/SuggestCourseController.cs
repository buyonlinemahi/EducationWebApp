using AutoMapper;
using DSuggestCourse = HCRGUniversityApp.Domain.Models.SuggestCourse;
using HCRGUniversityApp.Domain.ViewModels.SuggestCourseViewModel;
using HCRGUniversityApp.NEPService.CommonService;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;
using HCRGUniversityApp.Domain.Models.DiscountCouponModel;
using Braintree;
using HCRGUniversityApp.NEPService.ShippingPaymentService;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using HCRGUniversityApp.Domain.Models.SuggestCourse;

namespace HCRGUniversityApp.Controllers
{
    public class SuggestCourseController :  BaseController
    {
        private readonly ICommonService _commonService;
        public readonly IEncryption _encryptionService;
        // GET: SuggestCourse

        public SuggestCourseController(ICommonService commonService, IEncryption encryptionService)
        {
            _commonService = commonService;
            _encryptionService = encryptionService;
        }

        public ActionResult Index()
        {
            SuggestCourseViewModel _suggestCourseViewModel = new SuggestCourseViewModel();

            _suggestCourseViewModel.SuggestCourseResults = null;
            _suggestCourseViewModel.StateResults = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Common.State>>(_commonService.getAllState());

            return View(_suggestCourseViewModel);
        }

        [HttpPost]
        public ActionResult AddSuggestCourse(DSuggestCourse.SuggestCourse _suggestCourse)
        {
            if (_suggestCourse.SuggestCourseID > 0)
            {
                _suggestCourse.OrganizationID = Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]));
                _commonService.updateSuggestCourse(Mapper.Map<NEPService.CommonService.SuggestCourse>(_suggestCourse));
                return Json(GlobalConst.Message.UpdatedSuccessfully, GlobalConst.Message.text_html);
            }
            else
            {
                _suggestCourse.OrganizationID = Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]));
                var id = _commonService.addSuggestCourse(Mapper.Map<NEPService.CommonService.SuggestCourse>(_suggestCourse));
                return Json(GlobalConst.Message.AddedSuccessfully, GlobalConst.Message.text_html);
            }
        }
    }
}