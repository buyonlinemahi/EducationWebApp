using AutoMapper;
using HCRGUniversityApp.Domain.Models.Carousel;
using HCRGUniversityApp.Domain.Models.Education;
using HCRGUniversityApp.Domain.Models.EducationModel;
using HCRGUniversityApp.Domain.Models.Event;
using HCRGUniversityApp.Domain.Models.HomeContentModel;
using HCRGUniversityApp.Domain.Models.OrganizationModel;
using HCRGUniversityApp.Domain.Models.IndustryResearchModel;
using HCRGUniversityApp.Domain.Models.NewsModel;
using HCRGUniversityApp.Domain.ViewModels.HomeViewModel;
using HCRGUniversityApp.Domain.ViewModels.OrganizationViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/


        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.CollegeService.ICollegeService _collegeService;
        private readonly NEPService.NewsService.INewsService _newsService;
        public readonly IEncryption _encryptionService;
        public readonly NEPService.ClientService.IClientService _clientService;

        public HomeController(NEPService.EducationService.IEducationService educationService, NEPService.CollegeService.ICollegeService collegeService, NEPService.NewsService.INewsService newsService, IEncryption encryptionService, NEPService.ClientService.IClientService clientService)
        {
            _educationService = educationService;
            _collegeService = collegeService;
            _newsService = newsService;
            _encryptionService = encryptionService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            if (HCRGUser != null)
            {
                if (HCRGUser.PageName == "StudentPortal")
                    return RedirectToAction(GlobalConst.Actions.MyEducationController.Index, GlobalConst.Controllers.MyEducation, new { area = "" });
            }
            HomeViewModel homeModel = new HomeViewModel();
            HomeContent homecontent = new HomeContent();
            homecontent = Mapper.Map<HomeContent>(_newsService.GetHomeContent(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));

            string decodedHTML = HttpUtility.HtmlDecode(homecontent.HomePageContent);
            if (decodedHTML != null)
            {
                homeModel.HomePageContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                homeModel.HomePageContent = homeModel.HomePageContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                homeModel.HomePageContent = homeModel.HomePageContent.Replace("&nbsp;", "");
            }
            else
                homeModel.HomePageContent = decodedHTML;
            if (HCRGUser != null)
                homeModel.EducationResults = Mapper.Map<IEnumerable<Education>>(_educationService.GetEducationLatest(HCRGUser.UID, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            else
                homeModel.EducationResults = Mapper.Map<IEnumerable<Education>>(_educationService.GetEducationLatest(0, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));

            foreach (Education _education in homeModel.EducationResults)
                _education.EncrptedEducationID = _encryptionService.EncryptString2(_education.EducationID.ToString());
            homeModel.NewsResults = Mapper.Map<IEnumerable<News>>(_newsService.GetNewslatest(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            foreach (News viewmodel in homeModel.NewsResults)
            {
                viewmodel.NewsDateFormatted = viewmodel.NewsDate.ToString("D");
                viewmodel.EncryptedNewsID = _encryptionService.EncryptString2(viewmodel.NewsID.ToString());
            }
            homeModel.EventDetails = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Event.EventDetail>>(_newsService.GetEventsUpcoming(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            foreach (EventDetail eventViewmodel in homeModel.EventDetails)
            {
                if (eventViewmodel.EducationID != null)
                    eventViewmodel.pageUrl = GlobalConst.routeedURL.courseDetailPage + _encryptionService.EncryptString2(eventViewmodel.EducationID.ToString());
                else if (eventViewmodel.NewsID != null)
                    eventViewmodel.pageUrl = GlobalConst.routeedURL.newsDetailPage + _encryptionService.EncryptString2(eventViewmodel.NewsID.ToString()) + '/' + eventViewmodel.NewsType;
                else
                    eventViewmodel.pageUrl = "#";
            }
            return View(homeModel);
        }

        public PartialViewResult bindCarousel()
        {
            IEnumerable<CarouselDetail> _carousel = Mapper.Map<IEnumerable<CarouselDetail>>(_newsService.GetCarouselNewsDetail(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            foreach (CarouselDetail _carouselDetail in _carousel)
                _carouselDetail.EncryptedNewsID = _encryptionService.EncryptString2(_carouselDetail.NewsID.ToString());
            _carousel.ToList().ForEach(hp => hp.NewsPhotoUrl = GlobalConst.Message.SlashStoragePath + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])) + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.NewsPhotos + GlobalConst.ConstantChar.ForwardSlash + hp.NewsPhotoUrl);
            return PartialView("Carousel/_CarouselImagesPartial", _carousel);
        }

        public ActionResult ContactUs()
        {
            OrganizationContact _organizationViewModel = new OrganizationContact();
            var getOrganizationContacts = _clientService.GetSingleOrgContactByOrgID((HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (getOrganizationContacts != null)
            {
                _organizationViewModel = Mapper.Map<OrganizationContact>(getOrganizationContacts);
                var Pvalue = getOrganizationContacts.Phone;
                string _phoneFormat = MaskFormat(Pvalue.ToString());
                var Fvalue = getOrganizationContacts.Fax;
                string _faxFormat = MaskFormat(Fvalue.ToString());
                _organizationViewModel.Fax = _faxFormat;
                _organizationViewModel.Phone = _phoneFormat;
            }
            return View(_organizationViewModel);
        }

        public string MaskFormat(string arrray)
        {
            string finalString = "";
            for (int i = 0; i < arrray.Length; i++)
            {
                if (i == 0)
                    finalString += "(" + arrray[i];
                else if (i == 3)
                    finalString += ")" + arrray[i];
                else if (i == 6)
                    finalString += "-" + arrray[i];
                else
                    finalString += arrray[i];
            }
            return finalString;
        }
        public ActionResult IndustryResearch()
        {
            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("5"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage, GlobalConst.Controllers.User, new { area = "" });
                }
            }
            IndustryResearch industryResearch = new IndustryResearch();
            industryResearch = Mapper.Map<IndustryResearch>(_newsService.GetIndustryResearchContent(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            string decodedHTML = HttpUtility.HtmlDecode(industryResearch.IndustryResearchContent);
            if (decodedHTML != null)
            {
                industryResearch.IndustryResearchContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                industryResearch.IndustryResearchContent = industryResearch.IndustryResearchContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                industryResearch.IndustryResearchContent = industryResearch.IndustryResearchContent.Replace("&nbsp;", "");
            }
            else
                industryResearch.IndustryResearchContent = decodedHTML;
            return View(industryResearch);
        }

        [HttpPost]
        public ActionResult EducationNewsSearch(string search, int skip)
        {
            if (search == "%")
                search = "";
            PagedEducationNewsSearch educationNewsSearch = new PagedEducationNewsSearch();
            if (HCRGUser == null)
                educationNewsSearch = Mapper.Map<PagedEducationNewsSearch>(_educationService.GetEducationNewsSearchByTextPaged(search, 0, (HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))), skip, GlobalConst.Records.Take5));
            else
                educationNewsSearch = Mapper.Map<PagedEducationNewsSearch>(_educationService.GetEducationNewsSearchByTextPaged(search, HCRGUser.UID, (HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))), skip, GlobalConst.Records.Take5));
            foreach (EducationNewsSearch educationSearch in educationNewsSearch.educationNewsSearch)
            {
                if (educationSearch.NewsType == "")
                    educationSearch.pageUrl = GlobalConst.routeedURL.courseDetailPage + _encryptionService.EncryptString2(educationSearch.ID.ToString());
                else if (educationSearch.NewsType != "")
                {
                    Regex regex = new Regex("\\<[^\\>]*\\>");
                    educationSearch.descriptions = regex.Replace(HttpUtility.HtmlDecode(educationSearch.descriptions), String.Empty);
                    bool IsVideo = educationSearch.descriptions.Contains("Video");
                    if (IsVideo)
                        educationSearch.descriptions = "Video";
                    if (educationSearch.descriptions.Length > 350)
                    {
                        educationSearch.descriptions = regex.Replace(HttpUtility.HtmlDecode(educationSearch.descriptions), String.Empty).Substring(0, 350);
                        educationSearch.descriptions = educationSearch.descriptions.Replace("&nbsp;", "");
                    }
                    else
                        educationSearch.descriptions = educationSearch.descriptions.Replace("&nbsp;", "");
                    educationSearch.pageUrl = GlobalConst.routeedURL.newsDetailPage + _encryptionService.EncryptString2(educationSearch.ID.ToString()) + '/' + educationSearch.NewsType;
                }
                else
                    educationSearch.pageUrl = "#";
            }
            return Json(educationNewsSearch, GlobalConst.Message.text_html);
        }

    }
}
