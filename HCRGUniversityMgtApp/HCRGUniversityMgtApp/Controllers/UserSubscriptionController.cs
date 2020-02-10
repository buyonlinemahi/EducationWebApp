using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Client;
using HCRGUniversityMgtApp.Domain.Models.UserModel;
using HCRGUniversityMgtApp.Domain.ViewModels.UserSubscriptionViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class UserSubscriptionController : BaseController
    {

        private readonly NEPService.UserService.IUserService _userService;
        public readonly IEncryption _encryptionService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public UserSubscriptionController(NEPService.UserService.IUserService userService, IEncryption encryptionService, NEPService.ClientService.IClientService clientService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _clientService = clientService;

        }

        public ActionResult Index()
        {
            UserSubscription _userSubscription = new UserSubscription();
            // _userSubscription = Mapper.Map<UserSubscription>(_userService.GetUserSubscriptionDetails());
            string decodedHTML = "";
            string decodedHTML2 = "";

            if (_userSubscription != null)
            {
                decodedHTML = HttpUtility.HtmlDecode(_userSubscription.AllAccessTermsOfService);
                decodedHTML2 = HttpUtility.HtmlDecode(_userSubscription.EnterpriseTermsOfService);
            }

            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.LoadFormData(decodedHTML);
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");


            Editor1.FullScreen = false;
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.LoadFormData(decodedHTML2);
            Editor2.ClientFolder = "/richtexteditor/";
            Editor2.Width = Unit.Pixel(1050);
            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;
            Editor2.FullScreen = false;
            Editor2.DisabledItems = "save, help";
            string content2 = Request.Form["Editor2"];
            Editor2.MvcInit();
            ViewData["Editor2"] = Editor2.MvcGetString();
            Editor2.ResizeMode = RTEResizeMode.Disabled;

            return View();
        }

        [HttpPost]
        public ActionResult AddUserSubscription(UserSubscription _userSubscription)
        {
            UserSubscription userSubscription = new UserSubscription();
            if (_userSubscription.EncryptedUserSubscriptionID != null)
            {
                _userSubscription.UserSubscriptionID = Convert.ToInt32(DecryptString(_userSubscription.EncryptedUserSubscriptionID));
                userSubscription = Mapper.Map<UserSubscription>(_userService.GetUserSubscriptionByID(_userSubscription.UserSubscriptionID));
            }
            _userSubscription.CreatedOn = DateTime.Now;
            _userSubscription.IndividualAccessTermsOfService = _userSubscription.AllAccessTermsOfService;
            if (!_userSubscription.AllAccessIncludeProgram.HasValue)
                _userSubscription.AllAccessIncludeProgram = false;


            if (userSubscription != null)
            {
                if ((
                    _userSubscription.AllAccessParametersCoursePriceEnd == userSubscription.AllAccessParametersCoursePriceEnd)
                    && (_userSubscription.AllAccessParametersCoursePriceStart == userSubscription.AllAccessParametersCoursePriceStart)
                    && (_userSubscription.AllAccessPassPricing == userSubscription.AllAccessPassPricing)
                    && (_userSubscription.EnterprisePricing == userSubscription.EnterprisePricing)
                )
                {
                    _userSubscription.UserSubscriptionID = Convert.ToInt32(DecryptString(_userSubscription.EncryptedUserSubscriptionID)); ;
                    var _UserSubscriptionID = _userService.UpdateUserSubscription(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.UserSubscription>(_userSubscription));
                    _userSubscription.Mode = "Update";
                }
                else
                {
                    var OrgData = Mapper.Map<Organization>(_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID));
                    _userSubscription.OrganizationID = OrgData.OrganizationID;
                    var _UserSubscriptionID = _userService.AddUserSubscription(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.UserSubscription>(_userSubscription));
                    _userSubscription.Mode = "Add";
                }
            }
            else
            {
                var OrgData = Mapper.Map<Organization>(_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID));
                _userSubscription.OrganizationID = OrgData.OrganizationID;
                var _UserSubscriptionID = _userService.AddUserSubscription(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.UserSubscription>(_userSubscription));
                _userSubscription.Mode = "Add";
            }
            return Json(_userSubscription, GlobalConst.Message.text_html);
        }


        [HttpGet]
        public ActionResult AllSubcriptions()
        {
            UserSubscriptionViewModel _objuserSubscriptionViewModel = new UserSubscriptionViewModel();
            _objuserSubscriptionViewModel.UserSubscriptionResults = Mapper.Map<IEnumerable<UserSubscription>>(_userService.GetAllUserSubscriptionByOrganizationID(0, HCRGCLIENT.ClientID));
            foreach (var objorganizationResult in _objuserSubscriptionViewModel.UserSubscriptionResults)
                objorganizationResult.EncryptedUserSubscriptionID = EncryptString(objorganizationResult.UserSubscriptionID.ToString());

            if (HCRGCLIENT.ClientTypeID == 1)
                _objuserSubscriptionViewModel.IsHCRGAdmin = true;
            else
                _objuserSubscriptionViewModel.IsHCRGAdmin = false;
            return View(_objuserSubscriptionViewModel);
        }

        [HttpGet]
        public ActionResult EditUserSubscription(string _ID)
        {
            int Id = Convert.ToInt32(DecryptString(_ID));
            UserSubscription objuserSubscription = new UserSubscription();
            objuserSubscription = Mapper.Map<UserSubscription>(_userService.GetUserSubscriptionByID(Id));
            var OrgData = Mapper.Map<Organization>(_clientService.GetOrganizationByID(objuserSubscription.OrganizationID));
            objuserSubscription.OrganizationName = OrgData.OrganizationName;
            string decodedHTML = "";
            string decodedHTML2 = "";

            if (objuserSubscription != null)
            {
                decodedHTML = HttpUtility.HtmlDecode(objuserSubscription.AllAccessTermsOfService);
                decodedHTML2 = HttpUtility.HtmlDecode(objuserSubscription.EnterpriseTermsOfService);
            }

            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.LoadFormData(decodedHTML);
            Editor1.ClientFolder = "/richtexteditor/";

            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.FullScreen = false;
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];

            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.LoadFormData(decodedHTML2);
            Editor2.ClientFolder = "/richtexteditor/";

            Editor2.Width = Unit.Pixel(1050);
            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;


            Editor2.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor2.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor2.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor2.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor2.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor2.FullScreen = false;
            Editor2.DisabledItems = "save, help";
            string content2 = Request.Form["Editor2"];
            Editor2.MvcInit();

            ViewData["Editor2"] = Editor2.MvcGetString();
            Editor2.ResizeMode = RTEResizeMode.Disabled;
            objuserSubscription.EncryptedUserSubscriptionID = _ID;
            return View("Index", objuserSubscription);
        }

        [HttpPost]
        public JsonResult GetUserSubByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<UserSubscription>>(_userService.GetAllUserSubscriptionByOrganizationID(OrgID, HCRGCLIENT.ClientID));
            foreach (var objorganizationResult in data)
                objorganizationResult.EncryptedUserSubscriptionID = EncryptString(objorganizationResult.UserSubscriptionID.ToString());
            return Json(data);
        }
    }
}