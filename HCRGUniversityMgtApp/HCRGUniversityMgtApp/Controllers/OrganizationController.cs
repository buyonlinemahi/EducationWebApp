using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Client;
using HCRGUniversityMgtApp.Domain.Models.OrganizationContact;
using HCRGUniversityMgtApp.Domain.ViewModels.OrganizationViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{

    public class OrganizationController : BaseController
    {
        private readonly NEPService.ClientService.IClientService _clientService;
        StorageService _storageService;
        public readonly IEncryption _encryptionService;

        public OrganizationController(NEPService.ClientService.IClientService clientService, IEncryption encryptionService)
        {
            _clientService = clientService;
            _storageService = new StorageService();
            _encryptionService = encryptionService;
        }
        public ActionResult Index()
        {
            OrganizationViewModel _organizationViewModel = new OrganizationViewModel();
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationViewModel.OrganizationResults = Mapper.Map<IEnumerable<Organization>>(_clientService.getAllOrganization());
            else if (HCRGCLIENT.ClientTypeID == 2)
                _organizationViewModel.OrganizationResults = Mapper.Map<IEnumerable<Organization>>(_clientService.getOrganizationByOrganizationID(HCRGCLIENT.OrganizationID));
            else
                return RedirectToAction("Unauthorized", "Common");

            foreach (var objorganizationResult in _organizationViewModel.OrganizationResults)
                objorganizationResult.EncryptedOrganizationID = EncryptString(objorganizationResult.OrganizationID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationViewModel.IsHCRGAdmin = true;
            else
                _organizationViewModel.IsHCRGAdmin = false;
            return View(_organizationViewModel);
        }

        [HttpGet]
        public ActionResult AddOrganization()
        {
            return View();
        }

        public void setTheme(int themeID, int orgID, string LogoImage, string websiteName)
        {
            string srcPath = "";
            switch (themeID)
            {
                case 1:
                    srcPath = Server.MapPath("/Content/css/Theme/Theme1.css");
                    break;
                case 2:
                    srcPath = Server.MapPath("/Content/css/Theme/Theme2.css");
                    break;
                case 3:
                    srcPath = Server.MapPath("/Content/css/Theme/Theme3.css");
                    break;
                case 4:
                    srcPath = Server.MapPath("/Content/css/Theme/Theme4.css");
                    break;
                case 5:
                    srcPath = Server.MapPath("/Content/css/Theme/DefaultTheme.css");
                    break;
            }

            string path = _storageService.SetOrganizationCssStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + orgID + GlobalConst.ConstantChar.DoubleBackSlash + "CustomTheme"), "OrgTheme.css");
            _storageService.CreateDirectory(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + orgID + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.RTEUpload));

            string logoPath = "";
            if (LogoImage != null)
                logoPath = "/Storage/" + GlobalConst.FolderName.Org + orgID + "/LogoImage/" + LogoImage;

            using (FileStream stream = System.IO.File.OpenRead(srcPath))
            {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                System.IO.File.Create(path).Close();

                using (FileStream writeStream = System.IO.File.OpenWrite(path))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(writeStream);

                    // create a buffer to hold the bytes 
                    byte[] buffer = new Byte[1024];
                    int bytesRead;


                    // keep writing them to the output stream
                    while ((bytesRead = stream.Read(buffer, 0, 1024)) > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                    }
                }
            }

            string logoCss = @".spanWebsiteName:before {  content: '" + websiteName + @"';}
                     .application-title div {                     
                      box-sizing: border-box;
                      margin-top: 4px;                     
                      background: url(" + logoPath + ") no-repeat center; }";
            System.IO.File.AppendAllText(path, logoCss);
        }


        public ActionResult Detail(string _OrganizationID)
        {
            Organization objOrganization = new Organization();
            if (_OrganizationID != "")
            {
                int _id = Convert.ToInt16(DecryptString(_OrganizationID.ToString()));
                objOrganization = Mapper.Map<Organization>(_clientService.GetOrganizationByID(_id));
                objOrganization.LogoImage = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + objOrganization.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.LogoImage + GlobalConst.ConstantChar.ForwardSlash + objOrganization.LogoImage;
                if (objOrganization.FaviconImage != null)
                    objOrganization.FaviconImage = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + objOrganization.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.FaviconImage + GlobalConst.ConstantChar.ForwardSlash + objOrganization.FaviconImage;
                objOrganization.EncryptedOrganizationID = _OrganizationID;
                objOrganization.ClientTypeID = HCRGCLIENT.ClientTypeID;
            }
            else
            {
                objOrganization.ClientTypeID = HCRGCLIENT.ClientTypeID;
            }
            return View(objOrganization);
        }

        [HttpPost]
        public ActionResult AddOrganization(Organization _organization, string EncryptedOrganizationID)
        {
            string ImagePath = "";
            string FaviconImagePath = "";
            Organization _org = new Organization();
            if (EncryptedOrganizationID == "" || EncryptedOrganizationID == " ")
            {
                if (_organization.FaviconImageFile != null)
                    _organization.FaviconImage = "fav.ico";
                if (_organization.OrganizationImageFile != null)
                    _organization.LogoImage = "Org" + Path.GetExtension(_organization.OrganizationImageFile.FileName);
                else
                    _organization.LogoImage = "Org.png";
                _organization.OrganizationID = _clientService.AddOrganization(Mapper.Map<NEPService.ClientService.Organization>(_organization));
                //------- saving logo image
                string logoPathstring = _storageService.OrgainzationLogoDelete(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _organization.OrganizationID), Path.GetFileName(_organization.LogoImage));
                if (_organization.OrganizationImageFileContent != null)
                {
                    byte[] bytes = Convert.FromBase64String(_organization.OrganizationImageFileContent.Split(',')[1]);
                    if (System.IO.File.Exists(logoPathstring))
                        System.IO.File.Delete(logoPathstring);
                    using (System.IO.FileStream stream = new System.IO.FileStream(logoPathstring, System.IO.FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                else
                    SaveDefaultLogo(logoPathstring);
                //-----------------
                if (_organization.FaviconImageFile != null)
                {
                    FaviconImagePath = _storageService.SetOrganizationStorageFaviconPath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _organization.OrganizationID), _organization.FaviconImage);
                    _organization.FaviconImageFile.SaveAs(FaviconImagePath);
                }
                _org.Message = "Save SuccessFully";

            }
            else
            {
                _organization.OrganizationID = Convert.ToInt32(DecryptString(EncryptedOrganizationID));
                if (_organization.OrganizationImageFileContent != null)
                {
                    _organization.LogoImage = "Org" + Path.GetExtension(_organization.OrganizationImageFile.FileName);
                    //------- Saving logo image
                    string logoPathstring = _storageService.OrgainzationLogoDelete(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _organization.OrganizationID), Path.GetFileName(_organization.LogoImage));
                    byte[] bytes = Convert.FromBase64String(_organization.OrganizationImageFileContent.Split(',')[1]);
                    if (System.IO.File.Exists(logoPathstring))
                        System.IO.File.Delete(logoPathstring);
                    using (System.IO.FileStream stream = new System.IO.FileStream(logoPathstring, System.IO.FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                    //--------------------------------
                    ImagePath = _storageService.SetOrganizationStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _organization.OrganizationID), _organization.LogoImage);
                }
                else
                {
                    var _data = _clientService.GetOrganizationByID(_organization.OrganizationID);
                    _organization.LogoImage = _data.LogoImage;
                }

                if (_organization.FaviconImageFile != null)
                {
                    _organization.FaviconImage = "fav.ico";
                    FaviconImagePath = _storageService.SetOrganizationStorageFaviconPath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _organization.OrganizationID), _organization.FaviconImage);
                    _organization.FaviconImageFile.SaveAs(FaviconImagePath);
                }
                else
                {
                    var _data = _clientService.GetOrganizationByID(_organization.OrganizationID);
                    _organization.FaviconImage = _data.FaviconImage;
                }

                var OrganizationID = _clientService.UpdateOrganization(Mapper.Map<NEPService.ClientService.Organization>(_organization));
                _org.Message = "Update SuccessFully";
            }

            if (_organization.ThemeID != null)
                setTheme(_organization.ThemeID.Value, _organization.OrganizationID, _organization.LogoImage, _organization.WebsiteName);
            _org.EncryptedOrganizationID = EncryptString(_organization.OrganizationID.ToString());
            return Json(_org);
        }
        private void SaveDefaultLogo(string desPath)
        {
            int bufferSize = 1024 * 1024;
            using (FileStream fileStream = new FileStream(desPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            //using (FileStream fs = File.Open(<file-path>, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                FileStream fs = new FileStream(Server.MapPath("/Content/imgs/logo_new.png"), FileMode.Open, FileAccess.Read);
                fileStream.SetLength(fs.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                }
            }
        }

        #region OrganizationContact

        [HttpGet]
        public ActionResult OrgContactUsIndex()
        {
            OrganizationViewModel _organizationViewModel = new OrganizationViewModel();
            _organizationViewModel.OrganizationContactResults = Mapper.Map<IEnumerable<OrganizationContact>>(_clientService.GetOrganizationContactByOrganizationID(0, HCRGCLIENT.ClientID));
            foreach (var objorganizationResult in _organizationViewModel.OrganizationContactResults)
                objorganizationResult.EncryptedOrganizationContactID = EncryptString(objorganizationResult.OrganizationContactID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationViewModel.IsHCRGAdmin = true;
            else
                _organizationViewModel.IsHCRGAdmin = false;
            return View(_organizationViewModel);
        }

        [HttpPost]
        public JsonResult GetOrgContactByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<OrganizationContact>>(_clientService.GetOrganizationContactByOrganizationID(OrgID, HCRGCLIENT.ClientID));
            foreach (var objorganizationResult in data)
                objorganizationResult.EncryptedOrganizationContactID = EncryptString(objorganizationResult.OrganizationContactID.ToString());
            return Json(data);
        }


        [HttpGet]
        public ActionResult OrganizationContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrganizationContact(OrganizationContact _Contact)
        {
            _Contact.Phone = _Contact.Phone.Replace("(", String.Empty).Replace(")", String.Empty).Replace(" ", String.Empty).Replace("-", String.Empty);
            if (_Contact.Fax != null)
                _Contact.Fax = _Contact.Fax.Replace("(", String.Empty).Replace(")", String.Empty).Replace(" ", String.Empty).Replace("-", String.Empty);
            else
                _Contact.Fax = null;
            if (_Contact.EncryptedOrganizationContactID == null)
            {
                _Contact.OrganizationID = HCRGCLIENT.OrganizationID;
                _Contact.OrganizationContactID = _clientService.AddOrganizationContact(Mapper.Map<NEPService.ClientService.OrganizationContact>(_Contact));
                return Json(GlobalConst.Message.AddSucessfully, GlobalConst.Message.text_html);
            }
            else
            {
                _Contact.OrganizationContactID = Convert.ToInt32(DecryptString(_Contact.EncryptedOrganizationContactID));
                _Contact.OrganizationContactID = _clientService.UpdateOrganizationContact(Mapper.Map<NEPService.ClientService.OrganizationContact>(_Contact));
                return Json(GlobalConst.Message.UpdateSucessfully, GlobalConst.Message.text_html);
            }
        }
        [HttpGet]
        public ActionResult EditOrganizationContact(string _ID)
        {
            int Id = Convert.ToInt32(DecryptString(_ID));
            OrganizationContact objorganizationContact = new OrganizationContact();
            objorganizationContact = Mapper.Map<OrganizationContact>(_clientService.GetOrganizationContactByID(Id));
            objorganizationContact.EncryptedOrganizationContactID = (_ID);
            return View("OrganizationContact", objorganizationContact);
        }
        #endregion
    }
}