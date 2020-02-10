using AutoMapper;
using HCRGUniversityApp.Domain.Models.OrganizationModel;
using HCRGUniversityApp.Domain.Models.UserModel;
using HCRGUniversityApp.Domain.ViewModels.LoginViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HCRGUniversityApp.Controllers
{
    public class StudentPortalController : BaseController
    {
        private readonly NEPService.UserService.IUserService _userService;
        public readonly IEncryption _encryptionService;
        private readonly NEPService.LogSessionService.ILogSessionService _logSessionService;
        public readonly NEPService.CommonService.ICommonService _commonService;
        public readonly NEPService.ClientService.IClientService _clientService;

        public StudentPortalController(NEPService.UserService.IUserService userService, IEncryption encryptionService, NEPService.LogSessionService.ILogSessionService logSessionService, NEPService.CommonService.ICommonService commonService, NEPService.ClientService.IClientService clientService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _logSessionService = logSessionService;
            _commonService = commonService;
            _clientService = clientService;
        }
        [HttpGet]
        public ActionResult Index(string returnUrl, LoginViewModel model)
        {

            ModelState.Clear();


            if (Session["Org"] == null)
            {
                var _organizationID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
                Organization objOrganization = new Organization();
                objOrganization = Mapper.Map<Organization>(_clientService.GetOrganizationByID(_organizationID));
                objOrganization.LogoImage = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _organizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.LogoImage + GlobalConst.ConstantChar.ForwardSlash + objOrganization.LogoImage;
                Session["Org"] = objOrganization;
            }


            User user = new User();
            try
            {
                if (this.Request.RawUrl.ToLower() != "/studentportal/Index" && returnUrl == null)
                    model.PreviousLog = this.Request.UrlReferrer.AbsolutePath;
            }
            catch
            {
                model.PreviousLog = returnUrl;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model, string eventId, bool? Remember)
        {
            ModelState.Clear();
            model.User.RememberMe = Remember == null ? false : true;
            //eventId = model.PreviousLog;            
            HttpCookie cookie = new HttpCookie("Login");
            var user = _userService.GetUserByUserName(model.User.EmailID);
            if (user != null)
            {
                if (user.OrganizationID != Int32.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"].ToString())))
                {
                    model.InvalidMsg = GlobalResource.UserNotBelongToOrganization;
                    return View(GlobalConst.Views.Student.Index, model);
                }
                if (user.IsVerified == null || user.IsVerified == false)
                {
                    model.InvalidMsg = GlobalResource.NotVerified;
                    return View(GlobalConst.Views.Student.Index, model);
                }
                if (user.IsLocked)
                {
                    model.InvalidMsg = GlobalResource.UserIsLockMessage;
                    return View(GlobalConst.Views.Student.Index, model);
                }
                if (user.IsActive == false)
                {
                    model.InvalidMsg = GlobalResource.UserInActive;
                    return View(GlobalConst.Views.Student.Index, model);
                }
                if (model.User.EmailID == user.EmailID && _encryptionService.VerifyHashedPassword(model.User.Password, user.Password))
                {
                    HCRGUser = Mapper.Map<User>(user);

                    //HCRGUser.MenuIDs = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Common.Menu>>( _clientService.GetOrganizationMenuByOrganizationID(HCRGUser.OrganizationID));

                    HCRGUser.PageName = "StudentPortal";
                    _userService.ResetUserFailedAttemptCount(user);
                    _userService.UpdateUserSessionIDByUserID(user.UID, this.Session.SessionID);
                    _logSessionService.DeleteSessionAfterSchedule(HCRGUser.UID, Convert.ToInt32(_encryptionService.DecryptString2(ConfigurationManager.AppSettings[GlobalConst.Message.LogScheduleTime])));

                    if (eventId != "" && eventId.ToLower() == "/user/")
                        return RedirectToAction(GlobalConst.Actions.LoginController.Index, GlobalConst.Controllers.Home, new { area = "" });
                    if (eventId != "" && eventId.ToLower() != "/user/")
                    {
                        FormsAuthentication.SetAuthCookie(user.EmailID, model.User.RememberMe, model.User.Password);
                        Session["EmailID"] = user.EmailID;
                        Session["Password"] = model.User.Password;
                        if (model.User.RememberMe == true)
                        {

                            cookie.Values.Add("EmailID", user.EmailID);
                            cookie.Values.Add("Password", model.User.Password);
                            cookie.Expires = DateTime.Now.AddDays(15);
                            Response.Cookies.Add(cookie);

                        }
                        else if (model.User.RememberMe == false)
                        {
                            if (Request.Cookies["Login"] != null)
                            {
                                cookie.Values.Add("EmailID", "");
                                cookie.Values.Add("Password", "");
                                cookie.Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies.Add(cookie);
                            }

                        }
                        return Redirect(eventId);
                    }
                    else
                    {
                        var _res = _userService.getUserAllAccessPassByUserID(HCRGUser.UID);
                        if (_res != null)
                        {
                            if (_res.AllAccessEndDate.Value < DateTime.Now)
                            {
                                User _user1 = new User();
                                _user1 = Mapper.Map<User>(_userService.GetUserByID(HCRGUser.UID));
                                _user1.IsAllAccessPricing = null;
                                _user1.UserAllAccessPassID = null;
                                _userService.UpdateUser(Mapper.Map<HCRGUniversityApp.NEPService.UserService.User>(_user1));
                            }
                        }
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(user.EmailID, model.User.RememberMe, model.User.Password);
                            Session["EmailID"] = user.EmailID;
                            Session["Password"] = model.User.Password;
                            if (model.User.RememberMe == true)
                            {

                                cookie.Values.Add("EmailID", user.EmailID);
                                cookie.Values.Add("Password", model.User.Password);
                                cookie.Expires = DateTime.Now.AddDays(15);
                                Response.Cookies.Add(cookie);

                            }
                            else if (model.User.RememberMe == false)
                            {
                                if (Request.Cookies["Login"] != null)
                                {
                                    cookie.Values.Add("EmailID", "");
                                    cookie.Values.Add("Password", "");
                                    cookie.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(cookie);
                                }

                            }
                        }
                        var myVarproID = Session["MyprodcutID"];
                        if (myVarproID != null)
                            return RedirectToAction(GlobalConst.Actions.StoreController.ProductDetail, GlobalConst.Controllers.StoreController, new { pid = myVarproID });
                        else
                            return RedirectToAction(GlobalConst.Actions.MyEducationController.Index, GlobalConst.Controllers.MyEducation, new { area = "" });
                    }
                }
                else
                {
                    model.PreviousLog = eventId;
                    model.InvalidMsg = GlobalResource.InvalidPassword;
                    _userService.UpdateUserFailedAttemptCount(user);
                }
            }
            else
            {
                model.PreviousLog = eventId;
                model.InvalidMsg = GlobalResource.InvalidUserName;
            }
            model.User.Password = null;
            model.User.EmailID = null;
            return View(GlobalConst.Views.Student.Index, model);
        }

        public ActionResult LogOff()
        {
            _userService.ResetUserSessionID(HCRGUser.UID);
            Session.Abandon();
            return Redirect("/StudentPortal/Index");
        }
    }
}