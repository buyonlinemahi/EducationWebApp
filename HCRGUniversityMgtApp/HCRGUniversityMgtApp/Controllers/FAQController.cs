using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.FAQModel;
using HCRGUniversityMgtApp.Domain.ViewModels.FAQViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class FAQController : BaseController
    {
          private readonly NEPService.NewsService.INewsService _NewsService;
          private readonly NEPService.ClientService.IClientService _clientService;
          public FAQController(NEPService.NewsService.INewsService newsService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            FAQViewModel FaqModel = new FAQViewModel();
            FaqModel.FAQAllResults = Mapper.Map<IEnumerable<FAQ>>(_NewsService.GetAllFAQAccordingToOrganizationID(0, HCRGCLIENT.ClientID));
            var faqCategoryResults = _NewsService.GetAllFAQCategoriesAccordingToOrganizationID(0, HCRGCLIENT.ClientID);
            FaqModel.FAQCategoryResults = Mapper.Map<IEnumerable<FAQCategory>>(faqCategoryResults);
            if (HCRGCLIENT.ClientTypeID == 1)
                FaqModel.IsHCRGAdmin = true;
            else 
                FaqModel.IsHCRGAdmin = false;
           return View(FaqModel);
        }
        
        [HttpPost]
        public JsonResult GetFaqByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<FAQ>>(_NewsService.GetAllFAQAccordingToOrganizationID(OrgID, HCRGCLIENT.ClientID));
            var faqCategoryResults = _NewsService.GetAllFAQCategoriesAccordingToOrganizationID(OrgID, HCRGCLIENT.ClientID);
            foreach (FAQ obj in data)
            {
                var _FaqCategoryName = faqCategoryResults.Where(mahi => mahi.FAQCatID == obj.FAQCatID).Select(mahi => new { mahi.FAQCategoryTitle }).SingleOrDefault();
                obj.FaqCategoryName = _FaqCategoryName.FAQCategoryTitle.ToString();
            }
            return Json(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FAQ Faq, string hdFAQID)
        {
            if (hdFAQID == "")
            {
                var FaqID = _NewsService.AddFAQ(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.FAQ>(Faq));
                Faq.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                Faq.FAQID = FaqID;
                Faq.flag = true;
            }
            else
            {
                Faq.OrganizationID = Faq.OrganizationID;
                Faq.FAQID = Convert.ToInt32(hdFAQID);
                var FaqID = _NewsService.UpdateFAQ(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.FAQ>(Faq));
                Faq.flag = false;
            }
            return Json(Faq, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult AddFAQCategory(FAQCategory FaqCategory, string hdFAQCategoryID)
        {
            FaqCategory.OrganizationID = HCRGCLIENT.OrganizationID;
            if (hdFAQCategoryID == "" || hdFAQCategoryID == null)
            {
                var FAQCatID = _NewsService.AddFAQCategory(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.FAQCategory>(FaqCategory));
                FaqCategory.FAQCatID = FAQCatID;
                FaqCategory.flag = true;
            }
            else
            {
                FaqCategory.FAQCatID = Convert.ToInt32(hdFAQCategoryID);
                var FAQCatID = _NewsService.UpdateFAQCategory(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.FAQCategory>(FaqCategory));
                FaqCategory.flag = false;
            }
            return Json(FaqCategory, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult DeleteFAQCategory(FAQCategory faqcategory)
        {
            _NewsService.DeleteFAQCategory(faqcategory.FAQCatID);
            return Json("FAQ Category Deleted Successfully");
        }
        [HttpPost]
        public JsonResult DeleteFAQ(FAQ faq)
        {
            _NewsService.DeleteFAQ(faq.FAQID);
            return Json("FAQ Deleted Successfully");
        }
        [HttpPost]
        public JsonResult ShowAllFAQ(FAQ faq)
        {
            FAQViewModel FaqModel = new FAQViewModel();
          //  FaqModel.FAQResults = Mapper.Map<IEnumerable<FAQ>>(_NewsService.getAllFAQ());
            return Json(FaqModel);
        }


    }
}
