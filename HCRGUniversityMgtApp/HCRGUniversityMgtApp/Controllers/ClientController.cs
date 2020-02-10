using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Client;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using HCRGUniversityMgtApp.NEPService.CommonService;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{

    public class ClientController : BaseController
    {
        private readonly NEPService.ClientService.IClientService _clientService;
        public readonly IEncryption _encryptionService;
        StorageService _storageService;
        public readonly IEMail _mailService;
        private readonly ICommonService _commonService;

        public ClientController(NEPService.ClientService.IClientService clientService, IEncryption encryptionService, IEMail mailService, ICommonService commonService)
        {
            _commonService = commonService;
            _clientService = clientService;
            _storageService = new StorageService();
            _encryptionService = encryptionService;
            _mailService = mailService;
        }
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();
            
            PagedClient objClient = new PagedClient();
            if (HCRGCLIENT.ClientTypeID == 1)
                objClient = Mapper.Map<PagedClient>(_clientService.GetClientsOrganization());
            else
                objClient = Mapper.Map<PagedClient>(_clientService.GetClientsByOrganizationID(HCRGCLIENT.OrganizationID));
            foreach (var objClientResult in objClient.Clients)
                objClientResult.EncryptedClientID = EncryptString(objClientResult.ClientID.ToString());
            return View(objClient);
        }
        [HttpGet]
        public JsonResult GetAllClientsByPlanID(int _planID)
        {
            return Json(_clientService.GetClientsByPlanID(_planID), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetAllClients(int _clientID)
        {
            return Json(_clientService.GetClientsByPlanID(_clientID), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllOrganization()
        {
            if (HCRGCLIENT == null)
                return Json("");
            if (HCRGCLIENT.ClientTypeID == 1)
                return Json(_clientService.getAllOrganization(), GlobalConst.Message.text_html);
            else
                return Json(_clientService.getOrganizationByOrganizationID(HCRGCLIENT.OrganizationID), GlobalConst.Message.text_html);

        }

        [HttpPost]
        public ActionResult SaveClient(Client _client)
        {
            
            if (_client.ClientID == 0)
            {        
                string semail="" ;
                _client.ApproveOn = System.DateTime.Now;
                var clientInfo = _clientService.GetClientByClientName(_client.EmailID);
                if (clientInfo == null)
                {
                    _client.OrganizationID = HCRGCLIENT.OrganizationID;
                    if (_client.IsEmailSent != true)
                    {
                        string subject;
                        string Email;
                        string msg;
                        
                        if (_client.IsActive == true && _client.IsApproved == true)
                        {
                            _client.TempPassword = _encryptionService.GenratePassword(7);
                            _client.Password = _encryptionService.HashPassword(_client.TempPassword);
                            Email = _client.EmailID;
                            subject = "Client Registration & New Password";
                            msg = @"   <p>Hi " + _client.FirstName + " " + _client.LastName + @",</p>
                            <p>Your password is this : " + _client.TempPassword + @"</p>
                            <p>Thanks!</p>";
                            semail = _mailService.SendRandomPasswordEmail(msg, Email, subject);
                            
                        }
                    }
                    if (semail == GlobalConst.Message.EmailSentSuccessfully)
                    {
                        _client.IsEmailSent = true;
                        _client.ClientID = _clientService.AddClient(Mapper.Map<NEPService.ClientService.Client>(_client));


                        string folderPath = Server.MapPath(GlobalConst.FolderName.Storage);

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + _client.ClientID);
                        else
                        {
                            string path = folderPath + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + _client.ClientID;
                            Directory.CreateDirectory(path);
                        }
                        return Json(GlobalConst.Message.AddSucessfully, GlobalConst.Message.text_html);
                    }
                    else
                        return Json(semail, GlobalConst.Message.text_html);

                }
                else 
                    return Json(GlobalConst.Message.EmailExist, GlobalConst.Message.text_html);
            }
            else
            {
                string subject;
                string Email;
                string msg;
                
                string folderPath = Server.MapPath(GlobalConst.FolderName.Storage);
                if (_client.IsEmailSent != true) 
                {
                    if (_client.IsActive == true && _client.IsApproved == true)
                    {
                        _client.TempPassword = _encryptionService.GenratePassword(7);
                        _client.Password = _encryptionService.HashPassword(_client.TempPassword);
                        Email = _client.EmailID;
                        subject = "Client Registration & New Password";
                        msg = @"   <p>Hi " + _client.FirstName + " " + _client.LastName + @",</p>
                            <p>Your password is this : " + _client.TempPassword + @"</p>
                            <p>Thanks!</p>";
                        _mailService.SendRandomPasswordEmail(msg, Email, subject);
                        _client.IsEmailSent = true;
                    }
                }
                
                var clientModel =_clientService.GetClientByID(_client.ClientID);
                _client.Password = clientModel.Password;
                _client.ApproveOn = clientModel.ApproveOn;
                _client.ClientID = _clientService.UpdateClient(Mapper.Map<NEPService.ClientService.Client>(_client));
                return Json(GlobalConst.Message.UpdateSucessfully);
            }
        }

        [HttpPost]
        public ActionResult ResetClientPassword(int ClientId)
        {
            string subject;
            string msg;
            Client _client = new Client();
             var Client = Mapper.Map<Client>(_clientService.GetClientByID(ClientId));
             _client.TempPassword = _encryptionService.GenratePassword(7);
             Client.Password = _encryptionService.HashPassword(_client.TempPassword);
             subject = "Client Registration & New Password";
             msg = @" <p>Hi " + Client.FirstName + " " + Client.LastName + @",</p>
                            <p>Your password is this : " + _client.TempPassword + @"</p>
                            <p>Thanks!</p>";
             _mailService.SendRandomPasswordEmail(msg, Client.EmailID, subject);
             _clientService.ResetClientPassword(ClientId, Client.Password);
             return Json(GlobalConst.Message.ResetPassword);
        }

        [HttpPost]
        public ActionResult getAllClients()
        {

            PagedClient objClient = new PagedClient();
            if (HCRGCLIENT.ClientTypeID == 1)
                objClient = Mapper.Map<PagedClient>(_clientService.GetClientsOrganization());
            else
                objClient = Mapper.Map<PagedClient>(_clientService.GetClientsByOrganizationID(HCRGCLIENT.OrganizationID));
            foreach (var objClientResult in objClient.Clients)
                objClientResult.EncryptedClientID = EncryptString(objClientResult.ClientID.ToString());
            return Json(objClient, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ClientDetailByID(string ClientID)
        {
            Client objclient = new Client();
            objclient = Mapper.Map<Client>(_clientService.GetClientByID(int.Parse(ClientID)));
            return Json(objclient);
        }

        [HttpPost]
        public JsonResult GetNewClient(int skip)
        {
            try
            {
                PagedClient _clients = new PagedClient();
                _clients = Mapper.Map<PagedClient>(_clientService.GetClientVerifiedDetails(skip, GlobalConst.Records.Take));
                return Json(_clients, GlobalConst.Message.text_html);
            }
            catch  
            {
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult GetClientsByName(string searchtext)
        {
            PagedClient clientModel = new PagedClient();

            clientModel = Mapper.Map<PagedClient>(_clientService.GetClientsByName(searchtext == null ? "" : searchtext, GlobalConst.Records.Skip, GlobalConst.Records.take500, HCRGCLIENT.ClientID));
            clientModel.PagedRecords = GlobalConst.Records.LandingTake;
            var ClientTypes = _commonService.getAllClientType();
            foreach (Client obj in clientModel.Clients)
            {
                var _ClientTypeName = ClientTypes.Where(mahi => mahi.ClientTypeID == obj.ClientTypeID).Select(mahi => new { mahi.ClientTypeName }).SingleOrDefault();
                obj.ClientTypeName = _ClientTypeName.ClientTypeName.ToString();
            }
            return Json(clientModel, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult GetClientsByOrganizationID(int OrganizationID)
        {
            PagedClient _clients = new PagedClient();
            _clients = Mapper.Map<PagedClient>(_clientService.GetClientsByOrganizationID(HCRGCLIENT.OrganizationID));
            return Json(_clients.Clients);
        }

    }
}