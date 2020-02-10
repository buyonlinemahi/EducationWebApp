using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.DeliveryTerm;
using HCRGUniversityMgtApp.Domain.Models.HomeContentModel;
using HCRGUniversityMgtApp.Domain.Models.IndustryResearchModel;
using HCRGUniversityMgtApp.Domain.Models.NewsLetterModel;
using HCRGUniversityMgtApp.Domain.Models.Privacy;
using HCRGUniversityMgtApp.Domain.Models.ReturnPolicy;
using HCRGUniversityMgtApp.Domain.Models.TermsCondition;
using HCRGUniversityMgtApp.Domain.ViewModels.HomeContentViewModel;
using HCRGUniversityMgtApp.Domain.ViewModels.IndustryResearchViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class HomeContentController : BaseController
    {
        private readonly NEPService.NewsService.INewsService _NewsService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public HomeContentController(NEPService.NewsService.INewsService newsService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _clientService = clientService;
        }

        #region  HomeContent
        // GET: HomeContent

        public ActionResult Index()
        {
            return View(GetHomeContentData());
        }


        [HttpPost]
        public ActionResult GridData()
        {
            return Json(GetHomeContentData());
        }

        public ContentViewModel GetHomeContentData()
        {
            ContentViewModel _objHomeContentResult = new ContentViewModel();
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            _objHomeContentResult.HomeContentResults = Mapper.Map<IEnumerable<HomeContent>>(_NewsService.GetAllHomeContentByOrganizationID(HCRGCLIENT.ClientID, _organizationID));
            if (HCRGCLIENT.ClientTypeID == 1)
                _objHomeContentResult.IsHCRGAdmin = true;
            else
                _objHomeContentResult.IsHCRGAdmin = false;
            foreach (HomeContent _HomeContent in _objHomeContentResult.HomeContentResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _HomeContent.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_HomeContent.HomePageContent), String.Empty);
                _HomeContent.DescriptionShort = _HomeContent.DescriptionShort.Replace("&nbsp;", "");
                if (_HomeContent.DescriptionShort.Length > 1000)
                    _HomeContent.DescriptionShort = _HomeContent.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorContent = new Editor(System.Web.HttpContext.Current, "EditorContent");
            EditorContent.ClientFolder = "/richtexteditor/";
            EditorContent.Width = Unit.Pixel(1050);
            EditorContent.Height = Unit.Pixel(660);
            EditorContent.ResizeMode = RTEResizeMode.Disabled;

            EditorContent.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorContent.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorContent.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorContent.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorContent.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorContent.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorContent.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorContent.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorContent.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorContent.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorContent.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorContent.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorContent.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorContent.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorContent.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorContent.FullScreen = false;
            EditorContent.DisabledItems = "save, help";
            string content = Request.Form["EditorContent"];
            EditorContent.MvcInit();
            ViewData["EditorContent"] = EditorContent.MvcGetString();
            EditorContent.ResizeMode = RTEResizeMode.Disabled;
            return (_objHomeContentResult);
        }



        [HttpPost]
        public JsonResult GetHomeContentByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<HomeContent>>(_NewsService.GetAllHomeContentByOrganizationID(HCRGCLIENT.ClientID, OrgID));
            foreach (HomeContent _HomeContent in data)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _HomeContent.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_HomeContent.HomePageContent), String.Empty);
                _HomeContent.DescriptionShort = _HomeContent.DescriptionShort.Replace("&nbsp;", "");
                if (_HomeContent.DescriptionShort.Length > 1000)
                    _HomeContent.DescriptionShort = _HomeContent.DescriptionShort.Substring(0, 1000);
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult Update(HomeContent homeContent, string hdHomeContentID)
        {
            if (hdHomeContentID == "")
            {
                homeContent.OrganizationID = HCRGCLIENT.OrganizationID;
                var homeContentID = _NewsService.AddHomeContent(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.HomeContent>(homeContent));
                homeContent.HomeContentID = homeContentID;
                homeContent.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                homeContent.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(homeContent.HomePageContent), String.Empty);
                homeContent.DescriptionShort = homeContent.DescriptionShort.Replace("&nbsp;", "");
                if (homeContent.DescriptionShort.Length > 1000)
                    homeContent.DescriptionShort = homeContent.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                homeContent.HomeContentID = Convert.ToInt32(hdHomeContentID);
                var privacyPolicyID = _NewsService.UpdateHomeContent(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.HomeContent>(homeContent));
                homeContent.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                homeContent.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(homeContent.HomePageContent), String.Empty);
                homeContent.DescriptionShort = homeContent.DescriptionShort.Replace("&nbsp;", "");
                if (homeContent.DescriptionShort.Length > 1000)
                    homeContent.DescriptionShort = homeContent.DescriptionShort.Substring(0, 1000);
            }
            return Json(homeContent, GlobalConst.Message.text_html);
        }
        #endregion

        #region IndustryResearch
        public ActionResult IndustryResearch()
        {
            IndustryResearchViewModel _objIndustryResearchResults = new IndustryResearchViewModel();
            _objIndustryResearchResults.IndustryResearchResults = Mapper.Map<IEnumerable<IndustryResearch>>(_NewsService.GetAllIndustryResearchesByOrganizationID(0, HCRGCLIENT.ClientID));

            if (HCRGCLIENT.ClientTypeID == 1)
                _objIndustryResearchResults.IsHCRGAdmin = true;
            else
                _objIndustryResearchResults.IsHCRGAdmin = false;
            foreach (IndustryResearch _industryResearch in _objIndustryResearchResults.IndustryResearchResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _industryResearch.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_industryResearch.IndustryResearchContent), String.Empty);
                _industryResearch.DescriptionShort = _industryResearch.DescriptionShort.Replace("&nbsp;", "");
                if (_industryResearch.DescriptionShort.Length > 1000)
                    _industryResearch.DescriptionShort = _industryResearch.DescriptionShort.Substring(0, 1000);
            }

            Editor Editor = new Editor(System.Web.HttpContext.Current, "Editor");

            Editor.ClientFolder = "/richtexteditor/";
            Editor.Width = Unit.Pixel(1050);
            Editor.Height = Unit.Pixel(660);
            Editor.ResizeMode = RTEResizeMode.Disabled;
            Editor.FullScreen = false;



            Editor.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor.DisabledItems = "save, help";
            string content = Request.Form["Editor"];
            Editor.MvcInit();
            ViewData["Editor"] = Editor.MvcGetString();
            Editor.ResizeMode = RTEResizeMode.Disabled;
            return View(_objIndustryResearchResults);
        }
        [HttpPost]
        public ActionResult UpdateIndustryResearchContent(IndustryResearch industryResearch, string hdIndustryResearchID)
        {
            if (hdIndustryResearchID == "")
            {
                industryResearch.OrganizationID = HCRGCLIENT.OrganizationID;
                var industryResearchID = _NewsService.AddIndustryResearchContent(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.IndustryResearch>(industryResearch));
                industryResearch.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                industryResearch.IndustryResearchID = industryResearchID;
                industryResearch.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                industryResearch.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(industryResearch.IndustryResearchContent), String.Empty);
                industryResearch.DescriptionShort = industryResearch.DescriptionShort.Replace("&nbsp;", "");
                if (industryResearch.DescriptionShort.Length > 1000)
                    industryResearch.DescriptionShort = industryResearch.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                industryResearch.IndustryResearchID = Convert.ToInt32(hdIndustryResearchID);
                var privacyPolicyID = _NewsService.UpdateIndustryResearchContent(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.IndustryResearch>(industryResearch));
                industryResearch.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                industryResearch.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(industryResearch.IndustryResearchContent), String.Empty);
                industryResearch.DescriptionShort = industryResearch.DescriptionShort.Replace("&nbsp;", "");
                if (industryResearch.DescriptionShort.Length > 1000)
                    industryResearch.DescriptionShort = industryResearch.DescriptionShort.Substring(0, 1000);
            }
            return Json(industryResearch, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult GetIndustryResearchByOrganizationID(int organizationID)
        {
            IndustryResearchViewModel _objIndustryResearchResults = new IndustryResearchViewModel();
            _objIndustryResearchResults.IndustryResearchResults = Mapper.Map<IEnumerable<IndustryResearch>>(_NewsService.GetAllIndustryResearchesByOrganizationID(organizationID, HCRGCLIENT.ClientID));

            foreach (IndustryResearch _industryResearch in _objIndustryResearchResults.IndustryResearchResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _industryResearch.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_industryResearch.IndustryResearchContent), String.Empty);
                _industryResearch.DescriptionShort = _industryResearch.DescriptionShort.Replace("&nbsp;", "");
                if (_industryResearch.DescriptionShort.Length > 1000)
                    _industryResearch.DescriptionShort = _industryResearch.DescriptionShort.Substring(0, 1000);

            }
            Editor Editor = new Editor(System.Web.HttpContext.Current, "Editor");

            Editor.ClientFolder = "/richtexteditor/";
            Editor.Width = Unit.Pixel(1050);
            Editor.Height = Unit.Pixel(660);
            Editor.ResizeMode = RTEResizeMode.Disabled;
            Editor.FullScreen = false;




            Editor.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");


            Editor.DisabledItems = "save, help";
            string content = Request.Form["Editor"];
            Editor.MvcInit();
            ViewData["Editor"] = Editor.MvcGetString();
            Editor.ResizeMode = RTEResizeMode.Disabled;
            return Json(_objIndustryResearchResults);
        }
        #endregion

        #region Terms & Condition
        public ActionResult TermsCondition()
        {

            TermConditionViewModel _objtermConditionResult = new TermConditionViewModel();
            _objtermConditionResult.TermConditionsResults = Mapper.Map<IEnumerable<TermsCondition>>(_NewsService.GetAllTermAndConditionsAccordingToClientID(HCRGCLIENT.ClientID));
            foreach (TermsCondition _TermsCondition in _objtermConditionResult.TermConditionsResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _TermsCondition.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_TermsCondition.TermsConditionContent), String.Empty);
                _TermsCondition.DescriptionShort = _TermsCondition.DescriptionShort.Replace("&nbsp;", "");
                if (_TermsCondition.DescriptionShort.Length > 1000)
                    _TermsCondition.DescriptionShort = _TermsCondition.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorPolicy = new Editor(System.Web.HttpContext.Current, "EditorPolicy");
            EditorPolicy.ClientFolder = "/richtexteditor/";
            EditorPolicy.Width = Unit.Pixel(1050);
            EditorPolicy.Height = Unit.Pixel(660);
            EditorPolicy.ResizeMode = RTEResizeMode.Disabled;



            EditorPolicy.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorPolicy.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPolicy.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorPolicy.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorPolicy.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPolicy.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorPolicy.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorPolicy.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPolicy.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorPolicy.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorPolicy.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPolicy.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorPolicy.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorPolicy.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPolicy.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorPolicy.FullScreen = false;
            EditorPolicy.DisabledItems = "save, help";
            string content = Request.Form["EditorPolicy"];
            EditorPolicy.MvcInit();
            ViewData["EditorPolicy"] = EditorPolicy.MvcGetString();
            EditorPolicy.ResizeMode = RTEResizeMode.Disabled;
            return View(_objtermConditionResult);
        }
        [HttpPost]
        public ActionResult UpdateTermsCondition(TermsCondition termsCondition, string hdTermsConditionID)
        {
            if (hdTermsConditionID == "")
            {
                termsCondition.ClientID = HCRGCLIENT.ClientID;
                var termConditionID = _NewsService.AddTermsCondition(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.TermsCondition>(termsCondition));
                termsCondition.TermsConditionID = termConditionID;
                termsCondition.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                termsCondition.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(termsCondition.TermsConditionContent), String.Empty);
                termsCondition.DescriptionShort = termsCondition.DescriptionShort.Replace("&nbsp;", "");
                if (termsCondition.DescriptionShort.Length > 1000)
                    termsCondition.DescriptionShort = termsCondition.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                termsCondition.ClientID = HCRGCLIENT.ClientID;
                termsCondition.TermsConditionID = Convert.ToInt32(hdTermsConditionID);
                var termConditionID = _NewsService.UpdateTermsCondition(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.TermsCondition>(termsCondition));
                termsCondition.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                termsCondition.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(termsCondition.TermsConditionContent), String.Empty);
                termsCondition.DescriptionShort = termsCondition.DescriptionShort.Replace("&nbsp;", "");
                if (termsCondition.DescriptionShort.Length > 1000)
                    termsCondition.DescriptionShort = termsCondition.DescriptionShort.Substring(0, 1000);
            }
            return Json(termsCondition, GlobalConst.Message.text_html);
        }
        #endregion

        #region Privacy Policy
        public ActionResult PrivacyPolicy()
        {
            PrivacyPolicyViewModel _objprivacyPolicyResults = new PrivacyPolicyViewModel();
            _objprivacyPolicyResults.PrivacyPolicyResults = Mapper.Map<IEnumerable<PrivacyPolicy>>(_NewsService.GetAllPrivacyPolicyAccordingToOrganizationID(0, HCRGCLIENT.ClientID));
            if (HCRGCLIENT.ClientTypeID == 1)
                _objprivacyPolicyResults.IsHCRGAdmin = true;
            else
                _objprivacyPolicyResults.IsHCRGAdmin = false;
            foreach (PrivacyPolicy _privacy in _objprivacyPolicyResults.PrivacyPolicyResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _privacy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_privacy.PrivacyPolicyContent), String.Empty);
                _privacy.DescriptionShort = _privacy.DescriptionShort.Replace("&nbsp;", "");
                if (_privacy.DescriptionShort.Length > 1000)
                    _privacy.DescriptionShort = _privacy.DescriptionShort.Substring(0, 1000);
            }

            Editor EditorPrivacy = new Editor(System.Web.HttpContext.Current, "EditorPrivacy");

            EditorPrivacy.ClientFolder = "/richtexteditor/";
            EditorPrivacy.Width = Unit.Pixel(1050);
            EditorPrivacy.Height = Unit.Pixel(660);
            EditorPrivacy.ResizeMode = RTEResizeMode.Disabled;

            EditorPrivacy.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorPrivacy.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorPrivacy.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorPrivacy.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorPrivacy.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorPrivacy.FullScreen = false;
            EditorPrivacy.DisabledItems = "save, help";
            string content = Request.Form["EditorPrivacy"];
            EditorPrivacy.MvcInit();
            ViewData["EditorPrivacy"] = EditorPrivacy.MvcGetString();
            EditorPrivacy.ResizeMode = RTEResizeMode.Disabled;
            return View(_objprivacyPolicyResults);
        }
        [HttpPost]
        public ActionResult UpdatePrivacyPolicy(PrivacyPolicy privacy, string hdPrivacyPolicyID)
        {
            if (hdPrivacyPolicyID == "")
            {
                privacy.OrganizationID = HCRGCLIENT.OrganizationID;
                var privacyPolicyID = _NewsService.AddPrivacyPolicy(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.PrivacyPolicy>(privacy));
                privacy.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                privacy.PrivacyPolicyID = privacyPolicyID;
                privacy.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                privacy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(privacy.PrivacyPolicyContent), String.Empty);
                privacy.DescriptionShort = privacy.DescriptionShort.Replace("&nbsp;", "");
                if (privacy.DescriptionShort.Length > 1000)
                    privacy.DescriptionShort = privacy.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                privacy.PrivacyPolicyID = Convert.ToInt32(hdPrivacyPolicyID);
                var privacyPolicyID = _NewsService.UpdatePrivacyPolicy(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.PrivacyPolicy>(privacy));
                privacy.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                privacy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(privacy.PrivacyPolicyContent), String.Empty);
                privacy.DescriptionShort = privacy.DescriptionShort.Replace("&nbsp;", "");
                if (privacy.DescriptionShort.Length > 1000)
                    privacy.DescriptionShort = privacy.DescriptionShort.Substring(0, 1000);
            }
            return Json(privacy, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult GetPrivacyPolicyByOrganizationID(int organizationID)
        {
            PrivacyPolicyViewModel _objprivacyPolicyResults = new PrivacyPolicyViewModel();
            _objprivacyPolicyResults.PrivacyPolicyResults = Mapper.Map<IEnumerable<PrivacyPolicy>>(_NewsService.GetAllPrivacyPolicyAccordingToOrganizationID(organizationID, HCRGCLIENT.ClientID));

            foreach (PrivacyPolicy _privacy in _objprivacyPolicyResults.PrivacyPolicyResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _privacy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_privacy.PrivacyPolicyContent), String.Empty);
                _privacy.DescriptionShort = _privacy.DescriptionShort.Replace("&nbsp;", "");
                if (_privacy.DescriptionShort.Length > 1000)
                    _privacy.DescriptionShort = _privacy.DescriptionShort.Substring(0, 1000);
            }

            Editor EditorPrivacy = new Editor(System.Web.HttpContext.Current, "EditorPrivacy");

            EditorPrivacy.ClientFolder = "/richtexteditor/";
            EditorPrivacy.Width = Unit.Pixel(1050);
            EditorPrivacy.Height = Unit.Pixel(660);
            EditorPrivacy.ResizeMode = RTEResizeMode.Disabled;

            EditorPrivacy.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorPrivacy.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorPrivacy.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorPrivacy.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorPrivacy.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorPrivacy.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorPrivacy.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorPrivacy.FullScreen = false;
            EditorPrivacy.DisabledItems = "save, help";
            string content = Request.Form["EditorPrivacy"];
            EditorPrivacy.MvcInit();
            ViewData["EditorPrivacy"] = EditorPrivacy.MvcGetString();
            EditorPrivacy.ResizeMode = RTEResizeMode.Disabled;
            return Json(_objprivacyPolicyResults);
        }

        #endregion

        #region NewsLetter
        public ActionResult NewsLetter()
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            NewsLetterViewModel _objNewsLetterResult = new NewsLetterViewModel();
            if (HCRGCLIENT.ClientTypeID == 1)
                _objNewsLetterResult.IsHCRGAdmin = true;
            else
                _objNewsLetterResult.IsHCRGAdmin = false;
            _objNewsLetterResult.NewsLetterResutls = Mapper.Map<IEnumerable<NewsLetter>>(_NewsService.GetNewsLetterByClientID(HCRGCLIENT.ClientID, _organizationID));
            foreach (NewsLetter _NewsLetter in _objNewsLetterResult.NewsLetterResutls)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _NewsLetter.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_NewsLetter.NewsLetterContent), String.Empty);
                _NewsLetter.DescriptionShort = _NewsLetter.DescriptionShort.Replace("&nbsp;", "");
                if (_NewsLetter.DescriptionShort.Length > 1000)
                    _NewsLetter.DescriptionShort = _NewsLetter.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorReturnPolicy = new Editor(System.Web.HttpContext.Current, "EditorNewsLetter");
            EditorReturnPolicy.ClientFolder = "/richtexteditor/";
            EditorReturnPolicy.Width = Unit.Pixel(1050);
            EditorReturnPolicy.Height = Unit.Pixel(660);
            EditorReturnPolicy.ResizeMode = RTEResizeMode.Disabled;
            EditorReturnPolicy.FullScreen = false;

            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorReturnPolicy.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorReturnPolicy.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorReturnPolicy.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorReturnPolicy.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorReturnPolicy.DisabledItems = "save, help";
            string content = Request.Form["EditorNewsLetter"];
            EditorReturnPolicy.MvcInit();
            ViewData["EditorNewsLetter"] = EditorReturnPolicy.MvcGetString();
            EditorReturnPolicy.ResizeMode = RTEResizeMode.Disabled;
            return View(_objNewsLetterResult);
        }


        [HttpPost]
        public JsonResult GetNewsLetterByClientID(int orgID)
        {
            NewsLetterViewModel _objNewsLetterResult = new NewsLetterViewModel();
            _objNewsLetterResult.NewsLetterResutls = Mapper.Map<IEnumerable<NewsLetter>>(_NewsService.GetNewsLetterByClientID(HCRGCLIENT.ClientID, orgID));
            foreach (NewsLetter _NewsLetter in _objNewsLetterResult.NewsLetterResutls)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _NewsLetter.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_NewsLetter.NewsLetterContent), String.Empty);
                _NewsLetter.DescriptionShort = _NewsLetter.DescriptionShort.Replace("&nbsp;", "");
                if (_NewsLetter.DescriptionShort.Length > 1000)
                    _NewsLetter.DescriptionShort = _NewsLetter.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorReturnPolicy = new Editor(System.Web.HttpContext.Current, "EditorNewsLetter");
            EditorReturnPolicy.ClientFolder = "/richtexteditor/";
            EditorReturnPolicy.Width = Unit.Pixel(1050);
            EditorReturnPolicy.Height = Unit.Pixel(660);
            EditorReturnPolicy.ResizeMode = RTEResizeMode.Disabled;
            EditorReturnPolicy.FullScreen = false;

            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorReturnPolicy.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorReturnPolicy.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorReturnPolicy.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorReturnPolicy.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorReturnPolicy.DisabledItems = "save, help";
            string content = Request.Form["EditorNewsLetter"];
            EditorReturnPolicy.MvcInit();
            ViewData["EditorNewsLetter"] = EditorReturnPolicy.MvcGetString();
            EditorReturnPolicy.ResizeMode = RTEResizeMode.Disabled;
            return Json(_objNewsLetterResult);
        }

        [HttpPost]
        public ActionResult UpdateNewsLetter(NewsLetter newsLetter, string hdNewsLetterID)
        {
            NewsLetterViewModel NewsLetterModel = new NewsLetterViewModel();
            if (hdNewsLetterID == "")
            {
                newsLetter.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                if (newsLetter.NewsLetterContent == null)
                {
                    newsLetter.NewsLetterContent = String.Empty;
                    newsLetter.DescriptionShort = String.Empty;
                }
                else
                {
                    newsLetter.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(newsLetter.NewsLetterContent), String.Empty);
                    newsLetter.DescriptionShort = newsLetter.DescriptionShort.Replace("&nbsp;", "");
                    if (newsLetter.DescriptionShort.Length > 1000)
                        newsLetter.DescriptionShort = newsLetter.DescriptionShort.Substring(0, 1000);
                }
                newsLetter.OrganizationID = HCRGCLIENT.OrganizationID;
                newsLetter.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                var newsLetterID = _NewsService.AddNewsLetter(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsLetter>(newsLetter));
                newsLetter.NewsLetterID = newsLetterID;
            }
            else
            {

                newsLetter.NewsLetterID = Convert.ToInt32(hdNewsLetterID);
                var termConditionID = _NewsService.UpdateNewsLetter(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsLetter>(newsLetter));
                newsLetter.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                newsLetter.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(newsLetter.NewsLetterContent), String.Empty);
                newsLetter.DescriptionShort = newsLetter.DescriptionShort.Replace("&nbsp;", "");
                if (newsLetter.DescriptionShort.Length > 1000)
                    newsLetter.DescriptionShort = newsLetter.DescriptionShort.Substring(0, 1000);
            }
            return Json(newsLetter, GlobalConst.Message.text_html);
        }
        #endregion

        #region Return Policy
        public ActionResult ReturnPolicy()
        {
            ReturnPolicyViewModel _objReturnPolicyResult = new ReturnPolicyViewModel();
            _objReturnPolicyResult.ReturnPolicyResutls = Mapper.Map<IEnumerable<ReturnPolicy>>(_NewsService.GetAllReturnPolicysAccordingToClientID(HCRGCLIENT.ClientID));
            foreach (ReturnPolicy _ReturnPolicy in _objReturnPolicyResult.ReturnPolicyResutls)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _ReturnPolicy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_ReturnPolicy.ReturnPolicyContent), String.Empty);
                _ReturnPolicy.DescriptionShort = _ReturnPolicy.DescriptionShort.Replace("&nbsp;", "");
                if (_ReturnPolicy.DescriptionShort.Length > 1000)
                    _ReturnPolicy.DescriptionShort = _ReturnPolicy.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorReturnPolicy = new Editor(System.Web.HttpContext.Current, "EditorReturnPolicy");

            EditorReturnPolicy.ClientFolder = "/richtexteditor/";
            EditorReturnPolicy.Width = Unit.Pixel(1050);
            EditorReturnPolicy.Height = Unit.Pixel(660);
            EditorReturnPolicy.ResizeMode = RTEResizeMode.Disabled;

            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorReturnPolicy.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorReturnPolicy.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorReturnPolicy.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorReturnPolicy.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorReturnPolicy.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorReturnPolicy.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorReturnPolicy.FullScreen = false;
            EditorReturnPolicy.DisabledItems = "save, help";
            string content = Request.Form["EditorReturnPolicy"];
            EditorReturnPolicy.MvcInit();
            ViewData["EditorReturnPolicy"] = EditorReturnPolicy.MvcGetString();
            EditorReturnPolicy.ResizeMode = RTEResizeMode.Disabled;
            return View(_objReturnPolicyResult);
        }
        [HttpPost]
        public ActionResult UpdateReturnPolicy(ReturnPolicy ReturnPolicy, string hdReturnPolicyID)
        {
            ReturnPolicyViewModel termConditonModel = new ReturnPolicyViewModel();
            if (hdReturnPolicyID == "")
            {
                ReturnPolicy.ClientID = HCRGCLIENT.ClientID;
                var termConditionID = _NewsService.AddReturnPolicy(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.ReturnPolicy>(ReturnPolicy));
                ReturnPolicy.ReturnPolicyID = termConditionID;
                ReturnPolicy.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                ReturnPolicy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(ReturnPolicy.ReturnPolicyContent), String.Empty);
                ReturnPolicy.DescriptionShort = ReturnPolicy.DescriptionShort.Replace("&nbsp;", "");
                if (ReturnPolicy.DescriptionShort.Length > 1000)
                    ReturnPolicy.DescriptionShort = ReturnPolicy.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                ReturnPolicy.ClientID = HCRGCLIENT.ClientID;
                ReturnPolicy.ReturnPolicyID = Convert.ToInt32(hdReturnPolicyID);
                var termConditionID = _NewsService.UpdateReturnPolicy(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.ReturnPolicy>(ReturnPolicy));
                ReturnPolicy.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                ReturnPolicy.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(ReturnPolicy.ReturnPolicyContent), String.Empty);
                ReturnPolicy.DescriptionShort = ReturnPolicy.DescriptionShort.Replace("&nbsp;", "");
                if (ReturnPolicy.DescriptionShort.Length > 1000)
                    ReturnPolicy.DescriptionShort = ReturnPolicy.DescriptionShort.Substring(0, 1000);
            }
            return Json(ReturnPolicy, GlobalConst.Message.text_html);
        }
        #endregion

        #region Delivery Term
        public ActionResult DeliveryTerm()
        {
            DeliveryTermViewModel _objdeliveryResult = new DeliveryTermViewModel();
            _objdeliveryResult.DeliveryTermResults = Mapper.Map<IEnumerable<DeliveryTerm>>(_NewsService.GetAllDeliveryTermsAccordingToClientID(HCRGCLIENT.ClientID));

            foreach (DeliveryTerm _DeliveryTerm in _objdeliveryResult.DeliveryTermResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _DeliveryTerm.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_DeliveryTerm.DeliveryTermContents), String.Empty);
                _DeliveryTerm.DescriptionShort = _DeliveryTerm.DescriptionShort.Replace("&nbsp;", "");
                if (_DeliveryTerm.DescriptionShort.Length > 1000)
                    _DeliveryTerm.DescriptionShort = _DeliveryTerm.DescriptionShort.Substring(0, 1000);
            }

            Editor EditorDelivery = new Editor(System.Web.HttpContext.Current, "EditorDelivery");
            EditorDelivery.ClientFolder = "/richtexteditor/";
            EditorDelivery.Width = Unit.Pixel(1050);
            EditorDelivery.Height = Unit.Pixel(660);
            EditorDelivery.ResizeMode = RTEResizeMode.Disabled;

            EditorDelivery.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorDelivery.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorDelivery.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            EditorDelivery.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorDelivery.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorDelivery.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            EditorDelivery.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorDelivery.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorDelivery.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            EditorDelivery.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorDelivery.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorDelivery.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            EditorDelivery.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorDelivery.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorDelivery.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorDelivery.FullScreen = false;
            EditorDelivery.DisabledItems = "save, help";
            string content = Request.Form["EditorDelivery"];
            EditorDelivery.MvcInit();
            ViewData["EditorDelivery"] = EditorDelivery.MvcGetString();
            EditorDelivery.ResizeMode = RTEResizeMode.Disabled;
            return View(_objdeliveryResult);
        }
        [HttpPost]
        public ActionResult UpdateDeliveryTerm(DeliveryTerm deliveryTerm, string hdDeliveryConditionID)
        {
            if (hdDeliveryConditionID == "")
            {
                deliveryTerm.ClientID = HCRGCLIENT.ClientID;
                var termConditionID = _NewsService.AddDeliveryTerm(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.DeliveryTerm>(deliveryTerm));
                deliveryTerm.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                deliveryTerm.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(deliveryTerm.DeliveryTermContents), String.Empty);
                deliveryTerm.DescriptionShort = deliveryTerm.DescriptionShort.Replace("&nbsp;", "");
                if (deliveryTerm.DescriptionShort.Length > 1000)
                    deliveryTerm.DescriptionShort = deliveryTerm.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                deliveryTerm.ClientID = HCRGCLIENT.ClientID;
                deliveryTerm.DeliveryTermID = Convert.ToInt32(hdDeliveryConditionID);
                var termConditionID = _NewsService.UpdateDeliveryTerm(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.DeliveryTerm>(deliveryTerm));
                deliveryTerm.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                deliveryTerm.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(deliveryTerm.DeliveryTermContents), String.Empty);
                deliveryTerm.DescriptionShort = deliveryTerm.DescriptionShort.Replace("&nbsp;", "");
                if (deliveryTerm.DescriptionShort.Length > 1000)
                    deliveryTerm.DescriptionShort = deliveryTerm.DescriptionShort.Substring(0, 1000);
            }
            return Json(deliveryTerm, GlobalConst.Message.text_html);
        }
        #endregion
    }
}