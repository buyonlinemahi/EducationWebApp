using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Client;
using HCRGUniversityMgtApp.Domain.Models.UserModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    public class LoginController : BaseController
    {
        private readonly NEPService.ClientService.IClientService _clientService;
        public readonly IEncryption _encryptionService;
        public readonly IEMail _mailService;
        private readonly NEPService.UserService.IUserService _userService;


        public LoginController(NEPService.ClientService.IClientService clientService, IEncryption encryptionService, IEMail mailService, NEPService.UserService.IUserService userService)
        {
            _clientService = clientService;
            _encryptionService = encryptionService;
            _mailService = mailService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            User _user = new User();
            if (TempData["Message"] != null)
                _user.Message = TempData["Message"].ToString();
            return View(_user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Client _client)
        {
            ModelState.Clear();
            User _user = new User();
            var client = (_clientService.GetClientByClientName(_client.EmailID));
            if (client != null)
            {
                if (_client.EmailID == client.EmailID && _encryptionService.VerifyHashedPassword(_client.Password, client.Password))
                {
                    client.Password = null;
                    client.EmailID = null;
                    HCRGCLIENT = Mapper.Map<Client>(client);
                    int orgID = Int16.Parse(client.OrganizationID);
                    var _orgData = _clientService.GetOrganizationByID(orgID);
                    if (client.ClientTypeID == 2 && _orgData.MenuIDs != null)
                    {
                        var _menuIDS = _orgData.MenuIDs.Split(',');
                        HCRGCLIENT.ClientMenuIDS = _menuIDS.ToList();
                    }
                    _clientService.UpdateClientSessionIDByClientID(client.ClientID, this.Session.SessionID);
                    if (_orgData.LogoImage != null)
                        HCRGCLIENT.LogoPath = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + orgID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.LogoImage + GlobalConst.ConstantChar.ForwardSlash + _orgData.LogoImage;
                    return RedirectToAction(GlobalConst.Actions.HomeContentController.Index, GlobalConst.Controllers.Home, new { area = "" });
                }
            }
            TempData["Message"] = GlobalConst.Message.InvalidCredentials;
            return RedirectToAction("Index");
        }

        public ActionResult LogOff()
        {
            if (HCRGCLIENT != null)
            {
                _clientService.ResetClientSessionID(HCRGCLIENT.ClientID);
                Session.Abandon();
            }
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult ResetPassword(string Email)
        {
            string pass, passencrypt, msg;
            var message = "";
            var subject = "";
            var _client = (_clientService.GetClientByClientName(Email));

            if (_client != null)
            {
                if (_client.EmailID == Email)
                {
                    pass = _encryptionService.GenratePassword(7);

                    passencrypt = _encryptionService.HashPassword(pass);

                    _clientService.ResetClientPassword(_client.ClientID, passencrypt);

                    subject = "Reset Password";
                    msg = @"   <p>Hi " + _client.FirstName + " " + _client.LastName + @",</p>
                            <p>A password reset request has been recieved.Your new password is: " + pass + @"</p>
                            <p>Thanks</p>";
                    _mailService.SendEmail(msg, Email, subject);
                    message = "Yes";
                }
                else
                    message = "InvalidRegisteredEmail";
            }
            else
                message = "No";
            return Json(message, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult CleardLoginSession(string ClientID)
        {
            _clientService.ResetClientSessionID(Convert.ToInt32(DecryptString(ClientID)));
            Session.Clear();
            Session.Abandon();
            return Json("LoginSessoinCleared");
        }

        [HttpPost]
        public JsonResult ValidateLoginClientSession(string UserName)
        {
            var _res = _clientService.GetClientByClientName(UserName);
            if (_res != null)
                return Json((_res.ClientSessionID == null ? "na" : EncryptString(_res.ClientID.ToString())), JsonRequestBehavior.AllowGet);
            else
                return Json("na", JsonRequestBehavior.AllowGet);

        }
    }
}
