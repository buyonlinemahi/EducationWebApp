using AutoMapper;
using HCRGUniversityApp.Domain.Models.UserModel;
using HCRGUniversityApp.Domain.ViewModels.LoginViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using HCRGUniversityApp.NEPService.ClientService;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;


namespace HCRGUniversityApp.Controllers
{
    public class UserSubscriptionController : BaseController
    {
        // GET: UserSubscription
        public readonly IEncryption _encryptionService;
        private readonly NEPService.UserService.IUserService _userService;
        private readonly IClientService _clientService;
        public UserSubscriptionController(NEPService.UserService.IUserService userService, IClientService clientService, IEncryption encryptionService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _clientService = clientService;

        }

        public ActionResult Index()
        {

            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("6"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage, GlobalConst.Controllers.User, new { area = "" });
                }
            }

            UserSubscription _userSubscription = new UserSubscription();
            _userSubscription = Mapper.Map<UserSubscription>(_userService.GetUserSubscriptionDetails());
            return View(_userSubscription);
        }

        [HttpPost]
        public ActionResult getAllAccessTermsOfService()
        {
            string decodedHTML = HttpUtility.HtmlDecode(Mapper.Map<UserSubscription>(_userService.GetUserSubscriptionDetails()).AllAccessTermsOfService);
            if (decodedHTML != null)
            {
                decodedHTML = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                decodedHTML = decodedHTML.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                decodedHTML = decodedHTML.Replace("&nbsp;", "");

            }
            return Json(decodedHTML, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult getEnterprisePackage()
        {
            string decodedHTML = HttpUtility.HtmlDecode(Mapper.Map<UserSubscription>(_userService.GetUserSubscriptionDetails()).EnterpriseTermsOfService);
            if (decodedHTML != null)
            {
                decodedHTML = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                decodedHTML = decodedHTML.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                decodedHTML = decodedHTML.Replace("&nbsp;", "");
            }
            return Json(decodedHTML, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult AddAllAccessPass()
        {

            try
            {
                User _user = new User();
                _user = Mapper.Map<User>(_userService.GetUserByID(HCRGUser.UID));
                if (!_user.IsAllAccessPricing.HasValue)
                {
                    _user.IsAllAccessPricing = true;
                    _userService.UpdateUser(Mapper.Map<HCRGUniversityApp.NEPService.UserService.User>(_user));
                    return Json("Add", GlobalConst.Message.text_html);
                }
                else if (_user.IsAllAccessPricing.HasValue)
                {
                    if (_user.UserAllAccessPassID.HasValue)
                    {
                        var _res = _userService.getUserAllAccessPassByUserID(HCRGUser.UID);
                        if (_res.AllAccessEndDate.Value < DateTime.Now)
                        {
                            User _user1 = new User();
                            _user1 = Mapper.Map<User>(_userService.GetUserByID(HCRGUser.UID));
                            _user1.IsAllAccessPricing = null;
                            _user1.UserAllAccessPassID = null;
                            _userService.UpdateUser(Mapper.Map<HCRGUniversityApp.NEPService.UserService.User>(_user1));
                            return Json("Re-enrolled", GlobalConst.Message.text_html);
                        }
                        else
                            return Json("AlreadyExists", GlobalConst.Message.text_html);
                    }
                    else
                        return Json("AlreadyInCart", GlobalConst.Message.text_html);
                }
                else
                    return Json("AlreadyExists", GlobalConst.Message.text_html);
            }
            catch
            {
                return Json("Error", GlobalConst.Message.text_html);
            }
        }
    }
}