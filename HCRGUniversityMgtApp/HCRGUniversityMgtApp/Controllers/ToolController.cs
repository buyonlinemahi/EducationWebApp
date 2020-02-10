using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class ToolController : BaseController
    {
      
        public readonly IEncryption _encryptionService;
        public ToolController( IEncryption encryptionService)
        {
            _encryptionService = encryptionService;       
        }
      
        public ActionResult Index( )
        {
           return View();
        }


        [HttpPost]
        public ActionResult Index(string _InputData)
        {
            var EncryptedData = EncryptString(_InputData);
            return Json(EncryptedData);
        }


        [HttpPost]
        public ActionResult DecryptValues(string _InputData)
        {
            var DecryptData = DecryptString(_InputData);
            return Json(DecryptData);
        }
       



  

    }
}