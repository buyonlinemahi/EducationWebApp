using AutoMapper;
using HCRGUniversityApp.Domain.Models.UserModel;
using HCRGUniversityApp.Domain.ViewModels.LoginViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Text;
using System.Configuration;
using System.Web.Mvc;
using System.Web;
using System.Collections;
using System.Web.Security;
using CaptchaMvc.HtmlHelpers;
using System.Collections.Generic;
using HCRGUniversityApp.Domain.Models.Common;
using HCRGUniversityApp.NEPService.ClientService;

namespace HCRGUniversityApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly NEPService.UserService.IUserService _userService;
        private readonly NEPService.LogSessionService.ILogSessionService _logSessionService;
        public readonly IEncryption _encryptionService;
        public readonly IEMail _mailService;
        public readonly NEPService.CommonService.ICommonService _commonService;
        private readonly IClientService _clientService;

        public UserController(NEPService.UserService.IUserService userService, IEncryption encryptionService, IEMail mailService, NEPService.LogSessionService.ILogSessionService logSessionService, NEPService.CommonService.ICommonService commonService, IClientService clientService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _mailService = mailService;
            _logSessionService = logSessionService;
            _commonService = commonService;
            _clientService = clientService;
        }


        public ActionResult UnderConstruction()
        {
            return View();
        }

        public ActionResult Unauthorise()
        {
            return View();
        }

        public ActionResult UnauthorisePage()
        {
            return View();
        }

        public ActionResult ChangePassword(string userid)
        {
            string[] DecryptData = new string[1];
            DecryptData[0] = _encryptionService.DecryptString2(userid);
            var _data = DecryptData[0].Split(',');
            string _userId = _data[0];
            DateTime _datetime = Convert.ToDateTime(_data[1]);
            DateTime date1 = _datetime;
            DateTime date2 = System.DateTime.Now;
            TimeSpan ts = date2 - date1;
            User user = new User();
            user.UID = Convert.ToInt32(_userId);
            user.PasswordOTP = _encryptionService.EncryptString2(_data[2]);
            if (ts.TotalHours > 1)
                ViewBag.Message = GlobalConst.Message.TimeExpired;
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(User user, string TempPassword)
        {
            if (_encryptionService.DecryptString2(user.PasswordOTP).Trim() != user.PasswordOTP2.Trim())
                return Json("OTPNotMatched", GlobalConst.Message.text_html);

            bool matchpassword = _userService.matchUserResetTempPassword(user.UID, TempPassword);
            if (matchpassword == true)
            {
                _userService.UpdateUserPassword(user.UID, _encryptionService.HashPassword(user.Password));
                _userService.DeleteUserResetTempPassword(user.UID, TempPassword);
                return Json("PasswordUpdated", GlobalConst.Message.text_html);
            }
            else
                return Json("PasswordNotcorrect", GlobalConst.Message.text_html);
        }

        public ActionResult Index(string returnUrl, LoginViewModel model)
        {
            User user = new User();
            try
            {
                if (this.Request.RawUrl.ToLower() != "/user/" && this.Request.RawUrl.ToLower() != "/user/index" && returnUrl == null)
                    model.PreviousLog = this.Request.UrlReferrer.AbsolutePath;
                string _EmailId = Request.Cookies["Login"]["EmailID"];
                string _pass = Request.Cookies["Login"]["Password"];
                if (_EmailId != null && _EmailId != "")
                    user.EmailID = Request.Cookies["Login"]["EmailID"];
                if (_pass != null && _pass != "")
                    user.Password = Request.Cookies["Login"]["Password"];
                else
                    model.PreviousLog = returnUrl;
                model.User = user;
                model.PreviousLog = returnUrl;
            }
            catch
            {
                model.PreviousLog = returnUrl;
            }
            return View(model);
        }

        /// <summary>
        /// to verify user account.
        /// </summary>
        /// <param name="VerifyCode"></param>
        /// <returns></returns>
        public ActionResult EmailVerified(string VerifyCode)
        {
            User user = new User();
            user.UID = Convert.ToInt32(_encryptionService.DecryptString2(VerifyCode));
            _userService.updateUserVerification(user.UID, true);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User user, string eventId)
        {
            user.SpecialMenuIDs = GlobalConst.Menu.DefaultMenuID;
            user.OrganizationID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            user.ClientID = _userService.GetDefaulClientAdminByOrganizationID(user.OrganizationID);
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.PreviousLog = eventId;
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                user.IsActive = true;
                user.Password = _encryptionService.HashPassword(user.Password);

                var userID = _userService.AddUser(Mapper.Map<NEPService.UserService.User>(user));
                string _userID = _encryptionService.EncryptString2(userID.ToString());
                ViewBag.ErrMessage = "Success";
                string strOrgName = _clientService.GetOrganizationByID(user.OrganizationID).OrganizationName;
                string message = @"<p>Dear, " + user.FirstName + " " + user.LastName + @", Thank You for joining '" + strOrgName + "'. Below are your user account details. </p> <p>Please verify your email by clicking on this link <a href='" + GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash
                                    + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash
                                    + "User/EmailVerified?VerifyCode=" + _userID + @"'>Verify Account</a></p>
                                <p>Once verified, you will be able to log in and start your course.</p>
                                <p>Sincerely,</p>
                                <p>" + strOrgName + " Team</p>";
                _mailService.SendEmail("Verify Email", message, user.EmailID);
                ModelState.Clear();
                ModelState.Remove("key");
                return View("Index", new LoginViewModel());
                //return Json("Thanks for registration, an email has been sent to email account for verification.", GlobalConst.Message.text_html);
            }
            else
            {
                ViewBag.ErrMessage = "Captcha Invalid.";
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.Password = user.Password;
                ViewBag.EmailID = user.EmailID;
                ViewBag.ProfessionalTitle = user.ProfessionalTitle;
                ViewBag.Phone = user.Phone;
                ViewBag.Company = user.Company;
            }
            return View("Index", loginViewModel);
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
                    return View(GlobalConst.Views.User.Index, model);
                }
                if (user.IsVerified == null || user.IsVerified == false)
                {
                    model.InvalidMsg = GlobalResource.NotVerified;
                    return View(GlobalConst.Views.User.Index, model);
                }
                if (user.IsLocked)
                {
                    model.InvalidMsg = GlobalResource.UserIsLockMessage;
                    return View(GlobalConst.Views.User.Index, model);
                }
                if (user.IsActive == false)
                {
                    model.InvalidMsg = GlobalResource.UserInActive;
                    return View(GlobalConst.Views.User.Index, model);
                }
                if (model.User.EmailID == user.EmailID && _encryptionService.VerifyHashedPassword(model.User.Password, user.Password))
                {
                    user.Password = null;
                    user.EmailID = null;
                    HCRGUser = Mapper.Map<User>(user);

                    //HCRGUser.MenuIDs = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Common.Menu>>( _clientService.GetOrganizationMenuByOrganizationID(HCRGUser.OrganizationID));

                    HCRGUser.PageName = "WebPortal";
                    _userService.ResetUserFailedAttemptCount(user);
                    _userService.UpdateUserSessionIDByUserID(user.UID, this.Session.SessionID);
                    _logSessionService.DeleteSessionAfterSchedule(HCRGUser.UID, Convert.ToInt32(_encryptionService.DecryptString2(ConfigurationManager.AppSettings[GlobalConst.Message.LogScheduleTime])));

                    if (eventId != "" && eventId.ToLower() == "/user/")
                        return RedirectToAction(GlobalConst.Actions.LoginController.Index, GlobalConst.Controllers.Home, new { area = "" });
                    if (eventId != "" && eventId.ToLower() != "/user/")
                    {
                        FormsAuthentication.SetAuthCookie(model.User.EmailID, model.User.RememberMe, model.User.Password);
                        Session["EmailID"] = model.User.EmailID;
                        Session["Password"] = model.User.Password;
                        if (model.User.RememberMe == true)
                        {

                            cookie.Values.Add("EmailID", model.User.EmailID);
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
                        user.EmailID = null;
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
                            FormsAuthentication.SetAuthCookie(model.User.EmailID, model.User.RememberMe, model.User.Password);
                            Session["EmailID"] = model.User.EmailID;
                            Session["Password"] = model.User.Password;
                            if (model.User.RememberMe == true)
                            {

                                cookie.Values.Add("EmailID", model.User.EmailID);
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
                            //return RedirectToAction("Index", "Home");
                        }
                        
                        var myVarproID = Session["MyprodcutID"];
                        if (myVarproID != null)
                            return RedirectToAction(GlobalConst.Actions.StoreController.ProductDetail, GlobalConst.Controllers.StoreController, new { pid = myVarproID });
                        else
                            return RedirectToAction(GlobalConst.Actions.MyEducationController.Index, GlobalConst.Controllers.MyEducation, new { area = "" });
                    }
                    //if (cookie != null)
                    //{
                    //    user.EmailID = Request.Cookies["EmailID"].Value;
                    //    user.Password = Request.Cookies["Password"].Value;
                    //}

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
            return View(GlobalConst.Views.User.Index, model);
        }

        [HttpGet]
        public JsonResult UserExists(string value)
        {
            var message = "";
            var user = (_userService.GetUserByUserName(value));

            if (user != null)
            {
                if (user.EmailID == value)
                    message = "Yes";
                else
                    message = "No";
            }
            else
                message = "No";
            return Json(message, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LogOff()
        {
            if (HCRGUser != null)
            {
                _userService.ResetUserSessionID(HCRGUser.UID);
                Session.Abandon();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string Email)
        {
            string pass;
            var message = "";
            var user = (_userService.GetUserByUserName(Email));

            string _datetime = System.DateTime.Now.ToString();
            DateTime currentTime = DateTime.Now;
            DateTime x60MinsLater = currentTime.AddMinutes(60);
            if (user != null)
            {
                if (user.OrganizationID == Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))
                {
                    Random Generator = new Random();
                    String _strOTP = Generator.Next(0, 999999).ToString("D6");

                    pass = GenratePassword(8);
                    int tempid = _userService.AddUserResetTempPassword(user.UID, pass);
                    string _concatinate = user.UID.ToString() + "," + _datetime + "," + _strOTP;
                    string encryptedData = _encryptionService.EncryptString2(_concatinate);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("**THIS IS AN AUTOMATED MESSAGE - PLEASE DO NOT REPLY DIRECTLY TO THIS EMAIL**");
                    sb.Append("<br><br>");
                    sb.Append("Hello " + user.FirstName + " " + user.LastName + ",");
                    sb.Append("<br><br>");
                    sb.Append("Please use the following link and One Time Password to update your account.");
                    sb.Append("<br><br>");
                    sb.Append("Your temporary Password : " + pass);
                    sb.Append("<br>Please follow the link below and change your password.<br>");
                    sb.Append("<a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + "/User/ChangePassword?userid=" + encryptedData + "'>Change Password</a><br><br>");
                    sb.Append("Please use OTP " + _strOTP + " to change the password.");
                    sb.Append("<br>");
                    sb.Append("One Time Password and Link will expire in " + x60MinsLater);
                    sb.Append("<br><br>");
                    sb.Append("If you did not request this please disregard this email.");
                    sb.Append("<br><br>");
                    sb.Append("Thanks,<br>" + _clientService.GetOrganizationByID(user.OrganizationID).OrganizationName);
                    _mailService.SendEmail("Forgot Password", sb.ToString(), Email);
                    sb.Clear();
                    message = "Yes";
                }
                else
                    message = "The EmailID you have entered is not registered with the logged Organization ";
            }
            else
            {
                message = "No";
            }

            return Json(message, GlobalConst.Message.text_html);
        }

        public static string GenratePassword(int length)
        {
            string allowedLetterChars = GlobalConst.Message.allowedLetterChars;
            string allowedNumberChars = GlobalConst.Message.allowedNumberChars;
            char[] chars = new char[length];
            Random rd = new Random();

            bool useLetter = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetter)
                {
                    chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                    useLetter = false;
                }
                else
                {
                    chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                    useLetter = true;
                }

            }

            return new string(chars);
        }
        [HttpPost]
        public ActionResult CheckSession()
        {
            return Json(HCRGUser != null, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult CleardLoginSession(string UserID)
        {
            _userService.ResetUserSessionID(Convert.ToInt16(_encryptionService.DecryptString2(UserID)));
            Session.Clear();
            Session.Abandon();
            return Json("LoginSessoinCleared");
        }

        [HttpPost]
        public JsonResult ValidateLoginUserSession(string UserName)
        {
            var _res = _userService.GetUserByUserName(UserName);
            if (_res != null)
                return Json((_res.UserSessionID == null ? "na" : _encryptionService.EncryptString2(_res.UID.ToString())), JsonRequestBehavior.AllowGet);
            else
                return Json("na", JsonRequestBehavior.AllowGet);

        }
    }
}