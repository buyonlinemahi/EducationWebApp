using AutoMapper;
using HCRGUniversityApp.NEPService.CommonService;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using HCRGUniversityApp.NEPService.ClientService;

namespace HCRGUniversityApp.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ICommonService _commonService;
        private readonly IStorage _storageService;                
        private readonly IClientService _clientService;
        public readonly IEncryption _encryptionService;

        public CommonController(ICommonService commonService, IStorage storageService, IClientService clientService, IEncryption encryptionService)
        {
            _commonService = commonService;
            _storageService = storageService;            
            _clientService = clientService;
            _encryptionService = encryptionService;
          
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public JsonResult DecryptString()
        {
            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            return Json(_res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrganizationMenuByOrganizationID(int _organizationID)
        {
            var _resOrg = _clientService.GetOrganizationMenuByOrganizationID(_organizationID);
            return Json(_resOrg, JsonRequestBehavior.AllowGet);
        }
        


        public IEnumerable<HCRGUniversityApp.Domain.Models.Common.State> getAllState()
        {
            return Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Common.State>>(_commonService.getAllState());
        }

        public ActionResult ErrorViewer() 
        {
            //string VirtualStoragePath = _storageService.getVirtualPath();
            //string path = VirtualStoragePath + "/" + GlobalConst.MgmtDirectoryStructure.ErrorLog + "/";
            //var files = System.IO.Directory.GetFiles(path, "*.txt").Select(pat => System.IO.Path.GetFileName(pat)).ToArray();
            return View();
        }

        [HttpPost]
        public ActionResult GetFiles()
        {
            string VirtualStoragePath = _storageService.getVirtualPath();
            string path = VirtualStoragePath + "/" + GlobalConst.MgmtDirectoryStructure.ErrorLog + "/";
            string dirPath = path;
            List<string> files = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo fInfo in dirInfo.GetFiles())
                files.Add(fInfo.Name);
            return Json(files.ToArray());
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

        
    }
}